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
                throw new ArgumentException("Customer profile not found for logged-in user.");

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v =>
                    v.Id == dto.VehicleId &&
                    v.CustomerId == customer.Id);

            if (vehicle == null)
                throw new ArgumentException("Selected vehicle not found for this customer.");

            var appointment = new ServiceAppointment
            {
                CustomerId = customer.Id,
                VehicleId = dto.VehicleId,
                AppointmentDate = dto.AppointmentDate.Kind == DateTimeKind.Utc
                    ? dto.AppointmentDate
                    : DateTime.SpecifyKind(dto.AppointmentDate, DateTimeKind.Utc),
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

        public async Task<object> GetAllForStaffAsync()
        {
            return await _context.ServiceAppointments
                .Include(a => a.Customer)
                .Include(a => a.Vehicle)
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new
                {
                    a.Id,
                    a.AppointmentDate,
                    a.ServiceType,
                    a.Description,
                    a.Status,

                    CustomerId = a.CustomerId,
                    CustomerName = a.Customer != null ? a.Customer.FullName : "Unknown",
                    CustomerPhone = a.Customer != null ? a.Customer.Phone : "",
                    CustomerEmail = a.Customer != null ? a.Customer.Email : "",

                    VehicleId = a.VehicleId,
                    VehicleNumber = a.Vehicle != null ? a.Vehicle.VehicleNumber : "",
                    VehicleBrand = a.Vehicle != null ? a.Vehicle.VehicleBrand : "",
                    VehicleModel = a.Vehicle != null ? a.Vehicle.VehicleModel : ""
                })
                .ToListAsync();
        }

        public async Task<ServiceAppointment> UpdateStatusAsync(long appointmentId, string status)
        {
            var allowedStatuses = new[] { "Pending", "Confirmed", "Completed", "Cancelled" };

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.");

            if (!allowedStatuses.Contains(status))
                throw new ArgumentException("Invalid status. Allowed: Pending, Confirmed, Completed, Cancelled.");

            var appointment = await _context.ServiceAppointments.FindAsync(appointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            appointment.Status = status;

            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}