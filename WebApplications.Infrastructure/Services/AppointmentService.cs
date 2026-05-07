using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ServiceAppointment> CreateAppointmentForUserAsync(CreateAppointmentDto dto, long userId)
        {
            return await _appointmentRepository.CreateForCustomerUserIdAsync(dto, userId);
        }

        public Task<List<ServiceAppointment>> GetMyAppointmentsAsync(long userId)
        {
            return _appointmentRepository.GetByCustomerUserIdAsync(userId);
        }
    }
}