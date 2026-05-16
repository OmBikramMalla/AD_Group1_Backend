using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<ServiceAppointment> CreateForCustomerUserIdAsync(CreateAppointmentDto dto, long userId);
        Task<List<ServiceAppointment>> GetByCustomerUserIdAsync(long userId);

        Task<object> GetAllForStaffAsync();
        Task<ServiceAppointment> UpdateStatusAsync(long appointmentId, string status);
    }
}