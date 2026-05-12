using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    /// <summary>
    /// Handles all database operations related to staff management.
    /// Uses ASP.NET Identity's UserManager for user and role management.
    /// </summary>
    public class StaffRepository : IStaffRepository
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly AppDbContext _context;

        public StaffRepository(
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<object> GetAllStaffAsync()
        {
            // Get all users in the "Staff" role
            var staffUsers = await _userManager.GetUsersInRoleAsync("Staff");

            var result = staffUsers
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber,
                    Role = "Staff"
                });

            return result;
        }

        /// <inheritdoc/>
        public async Task<object?> GetStaffByIdAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            // Only return users who are Staff or Admin (not customers)
            if (!roles.Any(r => r == "Staff" || r == "Admin"))
                return null;

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };
        }

        /// <inheritdoc/>
        public async Task<object> RegisterStaffAsync(RegisterStaffDto dto)
        {
            // Validate role — only Staff is allowed via this endpoint
            var allowedRoles = new[] { "Staff", "Admin" };

            if (!allowedRoles.Contains(dto.Role))
                throw new Exception($"Invalid role '{dto.Role}'. Allowed values: Staff, Admin.");

            // Check for duplicate email
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("A user already exists with this email address.");

            // Create Identity user
            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.Phone
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // Ensure role exists, then assign it
            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new Roles { Name = dto.Role });

            await _userManager.AddToRoleAsync(user, dto.Role);

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = dto.Role
            };
        }

        /// <inheritdoc/>
        public async Task<object?> UpdateStaffAsync(long userId, UpdateStaffDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Only allow updating Staff or Admin accounts through this endpoint
            if (!currentRoles.Any(r => r == "Staff" || r == "Admin"))
                return null;

            // Update basic profile fields
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.Phone;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // Update role if it has changed
            var allowedRoles = new[] { "Staff", "Admin" };

            if (!allowedRoles.Contains(dto.Role))
                throw new Exception($"Invalid role '{dto.Role}'. Allowed values: Staff, Admin.");

            if (!currentRoles.Contains(dto.Role))
            {
                // Remove all current roles then assign the new one
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!await _roleManager.RoleExistsAsync(dto.Role))
                    await _roleManager.CreateAsync(new Roles { Name = dto.Role });

                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = dto.Role
            };
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteStaffAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var roles = await _userManager.GetRolesAsync(user);

            // Safety check: only delete Staff or Admin accounts via this endpoint
            if (!roles.Any(r => r == "Staff" || r == "Admin"))
                return false;

            var deleteResult = await _userManager.DeleteAsync(user);

            return deleteResult.Succeeded;
        }
    }
}
