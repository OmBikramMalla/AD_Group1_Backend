using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class AdminNotificationRepository : IAdminNotificationRepository
    {
        private readonly AppDbContext _context;

        public AdminNotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdminNotificationDto>> GetLowStockNotificationsAsync()
        {
            return await _context.Parts
                .AsNoTracking()
                .Where(p => p.StockQuantity < 10)
                .Select(p => new AdminNotificationDto
                {
                    Id = $"LOW-{p.Id}",
                    Type = "low_stock",
                    Title = $"Low Stock Alert: {p.PartName}",
                    Message = $"{p.PartName} has only {p.StockQuantity} units left. Please reorder soon.",
                    Priority = "high",
                    IsRead = false
                })
                .ToListAsync();
        }

        public async Task<List<AdminNotificationDto>> GetOverdueCreditNotificationsAsync()
        {
            return await _context.SalesInvoices
                .AsNoTracking()
                .Include(i => i.Customer)
                .Where(i =>
                    (i.TotalAmount - i.PaidAmount) > 0 &&
                    i.InvoiceDate <= DateTime.UtcNow.AddMonths(-1))
                .Select(i => new AdminNotificationDto
                {
                    Id = $"CREDIT-{i.Id}",
                    Type = "overdue_credit",
                    Title = $"Overdue Credit: Invoice #{i.Id}",
                    Message = $"{i.Customer!.FullName} has Rs. {i.TotalAmount - i.PaidAmount} unpaid for more than 1 month.",
                    Priority = "medium",
                    IsRead = false
                })
                .ToListAsync();
        }

        public async Task<List<SalesInvoice>> GetOverdueCreditInvoicesAsync()
        {
            return await _context.SalesInvoices
                .Include(i => i.Customer)
                .Where(i =>
                    (i.TotalAmount - i.PaidAmount) > 0 &&
                    i.InvoiceDate <= DateTime.UtcNow.AddMonths(-1) &&
                    i.Customer != null &&
                    !string.IsNullOrWhiteSpace(i.Customer.Email))
                .ToListAsync();
        }
    }
}