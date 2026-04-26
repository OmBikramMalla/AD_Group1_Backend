using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerByIdAsync(long id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<object> GetCustomerDetailsWithHistoryAsync(long id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Phone,
                    Vehicles = _context.Vehicles.Where(v => v.CustomerId == c.Id).ToList(),
                    Invoices = _context.SalesInvoices.Where(i => i.CustomerId == c.Id).ToList(),
                    Appointments = _context.ServiceAppointments.Where(a => a.CustomerId == c.Id).ToList()
                })
                .FirstOrDefaultAsync();

            return customer!;
        }
    }
}