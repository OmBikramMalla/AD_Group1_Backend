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

            invoice.TotalAmount = totalAmount;

            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}