using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceAppointment> CreateForCustomerUserIdAsync(CreateAppointmentDto dto, long userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                throw new Exception("Customer profile not found for logged-in user.");

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v =>
                    v.Id == dto.VehicleId &&
                    v.CustomerId == customer.Id);

            if (vehicle == null)
                throw new Exception("Selected vehicle not found for this customer.");

            var appointment = new ServiceAppointment
            {
                CustomerId = customer.Id,
                VehicleId = dto.VehicleId,
                AppointmentDate = dto.AppointmentDate,
                ServiceType = dto.ServiceType,
                Description = dto.Description,
                Status = "Pending"
            };

            _context.ServiceAppointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<List<ServiceAppointment>> GetByCustomerUserIdAsync(long userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                throw new Exception("Customer profile not found for logged-in user.");

            return await _context.ServiceAppointments
                .Where(a => a.CustomerId == customer.Id)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }
    }
}