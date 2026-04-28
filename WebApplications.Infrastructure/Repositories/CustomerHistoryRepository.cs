using Microsoft.EntityFrameworkCore;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
	public class CustomerHistoryRepository : ICustomerHistoryRepository
	{
		private readonly AppDbContext _context;

		public CustomerHistoryRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Customer?> GetCustomerHistoryAsync(long customerId)
		{
			return await _context.Customers
				.Include(c => c.SalesInvoices)
				.Include(c => c.ServiceAppointments)
				.Include(c => c.ServiceReviews)
				.FirstOrDefaultAsync(c => c.Id == customerId);
		}

		public async Task<Customer?> GetCustomerHistoryByEmailAsync(string email)
		{
			return await _context.Customers
				.Include(c => c.SalesInvoices)
				.Include(c => c.ServiceAppointments)
				.Include(c => c.ServiceReviews)
				.FirstOrDefaultAsync(c => c.Email == email);
		}
	}
}