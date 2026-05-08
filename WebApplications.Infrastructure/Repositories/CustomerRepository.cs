using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;

        public CustomerRepository(
            AppDbContext context,
            UserManager<Users> userManager,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<object> GetAllCustomersAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.FullName)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    c.Email,
                    Vehicles = c.Vehicles.Select(v => new
                    {
                        v.Id,
                        v.VehicleNumber,
                        v.VehicleModel,
                        v.VehicleBrand
                    }).ToList()
                })
                .ToListAsync();
        }

        public Task<Customer?> GetCustomerByIdAsync(long id)
        {
            return _context.Customers.FindAsync(id).AsTask();
        }

        public async Task<object?> GetCustomerDetailsWithHistoryAsync(long id)
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    c.Email,
                    Vehicles = _context.Vehicles.Where(v => v.CustomerId == c.Id).ToList(),
                    Invoices = _context.SalesInvoices.Where(i => i.CustomerId == c.Id).ToList(),
                    Appointments = _context.ServiceAppointments.Where(a => a.CustomerId == c.Id).ToList(),
                    PartRequests = _context.PartRequests.Where(p => p.CustomerId == c.Id).ToList(),
                    Reviews = _context.ServiceReviews.Where(r => r.CustomerId == c.Id).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("User already exists with this email.");

            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email.ToLower() == dto.Email.ToLower());

            if (existingCustomer != null)
                throw new Exception("Customer already exists with this email.");

            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            var customer = new Customer
            {
                UserId = user.Id,
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

            var emailBody = $@"Dear {dto.FullName},

Your customer account has been created successfully.

Login Credentials:

Email: {dto.Email}
Password: {dto.Password}

You can now:
- View invoices
- Book appointments
- Request unavailable parts
- View service history
- Submit service reviews

Please change your password after login.

Thank you,
Vehicle Parts Center";

            try
            {
                await _emailService.SendEmailAsync(
                    dto.Email,
                    "Your Customer Portal Account",
                    emailBody
                );
            }
            catch
            {
                // Email failed, but registration should still succeed.
            }

            return customer;
        }

        public async Task<object> SearchCustomersAsync(string query)
        {
            query = query.Trim().ToLower();

            return await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.Id.ToString().Contains(query) ||
                    c.FullName.ToLower().Contains(query) ||
                    c.Phone.ToLower().Contains(query) ||
                    c.Vehicles.Any(v =>
                        v.VehicleNumber.ToLower().Contains(query) ||
                        v.VehicleBrand.ToLower().Contains(query) ||
                        v.VehicleModel.ToLower().Contains(query)
                    )
                )
                .OrderBy(c => c.FullName)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    c.Email,
                    Vehicles = c.Vehicles.Select(v => new
                    {
                        v.Id,
                        v.VehicleNumber,
                        v.VehicleModel,
                        v.VehicleBrand
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<object?> GetMyProfileAsync(long userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    c.Email,
                    Vehicles = c.Vehicles.Select(v => new
                    {
                        v.Id,
                        v.VehicleNumber,
                        v.VehicleModel,
                        v.VehicleBrand
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<object?> UpdateMyProfileAsync(long userId, UpdateCustomerProfileDto dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return null;

            customer.FullName = dto.FullName;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;

            await _context.SaveChangesAsync();

            return await GetMyProfileAsync(userId);
        }

        public async Task<Vehicle> AddMyVehicleAsync(long userId, VehicleDto dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                throw new Exception("Customer profile not found.");

            var vehicle = new Vehicle
            {
                CustomerId = customer.Id,
                VehicleNumber = dto.VehicleNumber,
                VehicleModel = dto.VehicleModel,
                VehicleBrand = dto.VehicleBrand
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<Vehicle?> UpdateMyVehicleAsync(long userId, long vehicleId, VehicleDto dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return null;

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == vehicleId && v.CustomerId == customer.Id);

            if (vehicle == null)
                return null;

            vehicle.VehicleNumber = dto.VehicleNumber;
            vehicle.VehicleModel = dto.VehicleModel;
            vehicle.VehicleBrand = dto.VehicleBrand;

            await _context.SaveChangesAsync();

            return vehicle;
        }
    }
}