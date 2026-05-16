using Microsoft.AspNetCore.Identity;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
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

        public async Task<object> GetAllStaffAsync()
        {
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

        public async Task<object?> GetStaffByIdAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Staff"))
                return null;

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = "Staff"
            };
        }

        public async Task<object> RegisterStaffAsync(RegisterStaffDto dto)
        {
            if (dto.Role != "Staff")
                throw new Exception("Invalid role. Only Staff role is allowed.");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("A user already exists with this email address.");

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

            if (!await _roleManager.RoleExistsAsync("Staff"))
                await _roleManager.CreateAsync(new Roles { Name = "Staff" });

            await _userManager.AddToRoleAsync(user, "Staff");

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = "Staff"
            };
        }

        public async Task<object?> UpdateStaffAsync(long userId, UpdateStaffDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!currentRoles.Contains("Staff"))
                return null;

            if (dto.Role != "Staff")
                throw new Exception("Invalid role. Only Staff role is allowed.");

            user.FullName = dto.FullName;
            user.PhoneNumber = dto.Phone;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            if (!currentRoles.Contains("Staff"))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!await _roleManager.RoleExistsAsync("Staff"))
                    await _roleManager.CreateAsync(new Roles { Name = "Staff" });

                await _userManager.AddToRoleAsync(user, "Staff");
            }

            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                Role = "Staff"
            };
        }

        public async Task<bool> DeleteStaffAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Staff"))
                return false;

            var deleteResult = await _userManager.DeleteAsync(user);

            return deleteResult.Succeeded;
        }
    }
}