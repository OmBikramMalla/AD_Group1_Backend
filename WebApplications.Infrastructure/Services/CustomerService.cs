using Microsoft.EntityFrameworkCore;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;
using WebApplications.Application.DTOs;

namespace WebApplications.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(long id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<object?> GetCustomerDetailsWithHistoryAsync(long id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    c.Email,
                    Vehicles = _context.Vehicles.Where(v => v.CustomerId == c.Id).ToList(),
                    Invoices = _context.SalesInvoices.Where(i => i.CustomerId == c.Id).ToList(),
                    Appointments = _context.ServiceAppointments.Where(a => a.CustomerId == c.Id).ToList()
                })
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email
            };

            var vehicle = new Vehicle
            {
                VehicleNumber = dto.VehicleNumber,
                VehicleModel = dto.VehicleModel,
                VehicleBrand = dto.VehicleBrand,
                Customer = customer
            };

            _context.Customers.Add(customer);
            _context.Vehicles.Add(vehicle);

            await _context.SaveChangesAsync();

            return customer;
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