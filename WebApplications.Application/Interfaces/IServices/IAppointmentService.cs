using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IAppointmentService
    {
        Task<ServiceAppointment> CreateAppointmentForUserAsync(CreateAppointmentDto dto, long userId);
        Task<List<ServiceAppointment>> GetMyAppointmentsAsync(long userId);

        Task<object> GetAllForStaffAsync();
        Task<ServiceAppointment> UpdateStatusAsync(long appointmentId, string status);
    }
}