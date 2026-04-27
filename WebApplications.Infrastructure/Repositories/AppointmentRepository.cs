using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceAppointment> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            var appointment = new ServiceAppointment
            {
                CustomerId = dto.CustomerId,
                AppointmentDate = DateTime.SpecifyKind(dto.AppointmentDate, DateTimeKind.Utc),
                ServiceType = dto.ServiceType,
                Status = "Pending"
            };

            _context.ServiceAppointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}