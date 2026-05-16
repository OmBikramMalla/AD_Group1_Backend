using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // Top Spenders
        public async Task<List<CustomerReportDto>> GetTopSpendersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerReportDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    TotalSpent = c.SalesInvoices.Sum(i => i.PaidAmount)
                })
                .Where(c => c.TotalSpent > 0)
                .OrderByDescending(c => c.TotalSpent)
                .ToListAsync();
        }

        // Frequent Customers
        public async Task<List<CustomerReportDto>> GetFrequentCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerReportDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    TotalAppointments = c.ServiceAppointments.Count()
                })
                .OrderByDescending(c => c.TotalAppointments)
                .ToListAsync();
        }

        // Pending Payments
        public async Task<List<CustomerReportDto>> GetPendingPaymentsAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerReportDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    PendingAmount = c.SalesInvoices
                        .Sum(i => i.TotalAmount - i.PaidAmount)
                })
                .Where(c => c.PendingAmount > 0)
                .OrderByDescending(c => c.PendingAmount)
                .ToListAsync();
        }
    }
}