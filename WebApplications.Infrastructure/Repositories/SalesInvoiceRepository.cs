using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class SalesInvoiceRepository : ISalesInvoiceRepository
    {
        private readonly AppDbContext _context;

        public SalesInvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto)
        {
            var customer = await _context.Customers.FindAsync(dto.CustomerId);

            if (customer == null)
                throw new Exception("Customer not found.");

            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("At least one invoice item is required.");

            var invoice = new SalesInvoice
            {
                CustomerId = dto.CustomerId,
                PaidAmount = dto.PaidAmount,
                InvoiceDate = DateTime.UtcNow
            };

            decimal totalAmount = 0;

            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                var part = await _context.Parts.FindAsync(itemDto.PartId);

                if (part == null)
                    throw new Exception($"Part with ID {itemDto.PartId} not found.");

                if (part.StockQuantity < itemDto.Quantity)
                    throw new Exception($"Not enough stock for {part.PartName}.");

                var invoiceItem = new SalesInvoiceItem
                {
                    PartId = part.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = part.Price
                };

                totalAmount += part.Price * itemDto.Quantity;

                part.StockQuantity -= itemDto.Quantity;

                invoice.Items.Add(invoiceItem);
            }

            var originalTotal = totalAmount;
            var discountAmount = originalTotal > 5000 ? originalTotal * 0.10m : 0;
            var finalTotal = originalTotal - discountAmount;

            if (dto.PaidAmount < 0)
                throw new Exception("Paid amount cannot be negative.");

            if (dto.PaidAmount > finalTotal)
                throw new Exception("Paid amount cannot be greater than total amount.");

            invoice.TotalAmount = finalTotal;
            invoice.PaidAmount = dto.PaidAmount;

            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
        public async Task<SalesInvoice?> GetInvoiceWithDetailsAsync(long id)
        {
            return await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                    .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<object> GetRecentInvoicesAsync()
        {
            return await _context.SalesInvoices
                .Include(i => i.Customer)
                .OrderByDescending(i => i.InvoiceDate)
                .Take(5)
                .Select(i => new
                {
                    i.Id,
                    i.InvoiceDate,
                    i.TotalAmount,
                    i.PaidAmount,
                    DueAmount = i.TotalAmount - i.PaidAmount,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer != null ? i.Customer.FullName : "Unknown",
                    Status = (i.TotalAmount - i.PaidAmount) > 0 ? "Unpaid" : "Completed"
                })
                .ToListAsync();
        }

        public async Task<object> GetSalesSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;

            var invoices = await _context.SalesInvoices
                .AsNoTracking()
                .ToListAsync();

            var todayInvoices = invoices
                .Where(i => i.InvoiceDate.Date == today)
                .ToList();

            return new
            {
                TodaySales = todayInvoices.Sum(i => i.PaidAmount),
                TodayTransactions = todayInvoices.Count,
                TotalInvoices = invoices.Count,
                PendingInvoices = invoices.Count(i => (i.TotalAmount - i.PaidAmount) > 0),
                TotalPendingAmount = invoices.Sum(i => i.TotalAmount - i.PaidAmount)
            };
        }

        public async Task<object> GetAllInvoicesAsync()
        {
            return await _context.SalesInvoices
                .Include(i => i.Customer)
                .OrderByDescending(i => i.InvoiceDate)
                .Select(i => new
                {
                    i.Id,
                    i.InvoiceDate,
                    i.TotalAmount,
                    i.PaidAmount,
                    DueAmount = i.TotalAmount - i.PaidAmount,
                    CustomerName = i.Customer != null ? i.Customer.FullName : "Unknown",
                    CustomerEmail = i.Customer != null ? i.Customer.Email : "",
                    Status = (i.TotalAmount - i.PaidAmount) > 0 ? "Unpaid" : "Completed"
                })
                .ToListAsync();
        }
    }
}