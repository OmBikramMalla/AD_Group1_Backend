using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    /// <summary>
    /// Admin-only controller for managing staff registration and roles.
    /// Feature 2: Admin can manage staff registration and roles.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/admin/staff")]
    [ApiController]
    public class StaffManagementController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffManagementController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // GET: api/admin/staff
        // Returns all registered staff members with their roles.
        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _staffService.GetAllStaffAsync();
            return Ok(staff);
        }

        // GET: api/admin/staff/{id}
        // Returns a specific staff member by their user ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(long id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);

            if (staff == null)
                return NotFound(new { message = "Staff member not found." });

            return Ok(staff);
        }

        // POST: api/admin/staff
        // Registers a new staff member. Only Admin can perform this action.
        [HttpPost]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest(new { message = "Full name is required." });

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { message = "Email is required." });

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Password is required." });

            try
            {
                var result = await _staffService.RegisterStaffAsync(dto);
                return Ok(new { message = "Staff member registered successfully.", staff = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/admin/staff/{id}
        // Updates a staff member's details and/or role.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(long id, [FromBody] UpdateStaffDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest(new { message = "Full name is required." });

            try
            {
                var result = await _staffService.UpdateStaffAsync(id, dto);

                if (result == null)
                    return NotFound(new { message = "Staff member not found." });

                return Ok(new { message = "Staff member updated successfully.", staff = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/admin/staff/{id}
        // Permanently deletes a staff member's account.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(long id)
        {
            try
            {
                var deleted = await _staffService.DeleteStaffAsync(id);

                if (!deleted)
                    return NotFound(new { message = "Staff member not found." });

                return Ok(new { message = "Staff member deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
