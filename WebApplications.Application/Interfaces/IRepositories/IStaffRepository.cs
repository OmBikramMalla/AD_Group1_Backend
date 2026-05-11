using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IRepositories
{
    /// <summary>
    /// Repository interface for staff data operations.
    /// </summary>
    public interface IStaffRepository
    {
        /// <summary>Gets all staff members with their roles.</summary>
        Task<object> GetAllStaffAsync();

        /// <summary>Gets a single staff member by their user ID.</summary>
        Task<object?> GetStaffByIdAsync(long userId);

        /// <summary>Registers a new staff user account.</summary>
        Task<object> RegisterStaffAsync(RegisterStaffDto dto);

        /// <summary>Updates a staff member's details and/or role.</summary>
        Task<object?> UpdateStaffAsync(long userId, UpdateStaffDto dto);

        /// <summary>Deletes a staff member's account.</summary>
        Task<bool> DeleteStaffAsync(long userId);
    }
}
