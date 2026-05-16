using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Helpers;
using WebApplications.Infrastructure.Presistance;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplications.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly JwtHelper _jwtHelper;
        private readonly AppDbContext _context;

        public AuthController(
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            SignInManager<Users> signInManager,
            JwtHelper jwtHelper,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required.");

            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest("Full name is required.");

            var allowedRoles = new[] { "Admin", "Staff", "Customer" };

            if (!allowedRoles.Contains(dto.Role))
                return BadRequest("Invalid role.");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return BadRequest("User already exists with this email.");

            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(new Roles
                {
                    Name = dto.Role
                });
            }

            await _userManager.AddToRoleAsync(user, dto.Role);

            if (dto.Role == "Customer")
            {
                var customer = new Customer
                {
                    UserId = user.Id,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Phone = dto.Phone ?? ""
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                message = "User registered successfully.",
                userId = user.Id,
                user.FullName,
                user.Email,
                role = dto.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Customer";

            var token = _jwtHelper.GenerateToken(user, role);

            return Ok(new
            {
                message = "Login successful.",
                token,
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    role
                }
            });
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized(new { message = "Invalid token." });

            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized(new { message = "Invalid user id in token." });

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound(new { message = "User not found." });

            if (string.IsNullOrWhiteSpace(dto.CurrentPassword))
                return BadRequest(new { message = "Current password is required." });

            if (string.IsNullOrWhiteSpace(dto.NewPassword))
                return BadRequest(new { message = "New password is required." });

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest(new { message = "New password and confirm password do not match." });

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
                return BadRequest(new
                {
                    message = "Failed to change password.",
                    errors = result.Errors.Select(e => e.Description)
                });

            return Ok(new { message = "Password changed successfully." });
        }
    }
}