using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<ServiceAppointment> CreateForCustomerUserIdAsync(CreateAppointmentDto dto, long userId);
    }
}