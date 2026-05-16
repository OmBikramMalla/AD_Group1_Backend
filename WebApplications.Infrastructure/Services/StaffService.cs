using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Infrastructure.Services
{
    /// <summary>
    /// Delegates staff management business logic to the staff repository.
    /// </summary>
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        /// <inheritdoc/>
        public Task<object> GetAllStaffAsync()
        {
            return _staffRepository.GetAllStaffAsync();
        }

        /// <inheritdoc/>
        public Task<object?> GetStaffByIdAsync(long userId)
        {
            return _staffRepository.GetStaffByIdAsync(userId);
        }

        /// <inheritdoc/>
        public Task<object> RegisterStaffAsync(RegisterStaffDto dto)
        {
            return _staffRepository.RegisterStaffAsync(dto);
        }

        /// <inheritdoc/>
        public Task<object?> UpdateStaffAsync(long userId, UpdateStaffDto dto)
        {
            return _staffRepository.UpdateStaffAsync(userId, dto);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteStaffAsync(long userId)
        {
            return _staffRepository.DeleteStaffAsync(userId);
        }
    }
}
