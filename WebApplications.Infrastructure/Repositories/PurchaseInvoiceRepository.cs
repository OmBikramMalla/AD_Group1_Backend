using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly AppDbContext _context;

        public PurchaseInvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _context.PurchaseInvoices
                .Include(pi => pi.Vendor)
                .OrderByDescending(pi => pi.InvoiceDate)
                .Select(pi => new
                {
                    pi.Id,
                    pi.InvoiceDate,
                    pi.TotalAmount,
                    pi.Notes,
                    VendorName = pi.Vendor != null ? pi.Vendor.VendorName : "Unknown"
                })
                .ToListAsync();
        }

        public async Task<object?> GetByIdAsync(long id)
        {
            return await _context.PurchaseInvoices
                .Include(pi => pi.Vendor)
                .Include(pi => pi.Items)
                    .ThenInclude(item => item.Part)
                .Where(pi => pi.Id == id)
                .Select(pi => new
                {
                    pi.Id,
                    pi.InvoiceDate,
                    pi.TotalAmount,
                    pi.Notes,
                    Vendor = pi.Vendor,
                    Items = pi.Items.Select(i => new
                    {
                        i.Id,
                        i.Quantity,
                        i.UnitCost,
                        PartName = i.Part != null ? i.Part.PartName : "Unknown"
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PurchaseInvoice> CreateAsync(CreatePurchaseInvoiceDto dto)
        {
            var vendor = await _context.Vendors.FindAsync(dto.VendorId);
            if (vendor == null)
                throw new Exception("Vendor not found.");

            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("At least one item is required for a purchase invoice.");

            var invoice = new PurchaseInvoice
            {
                VendorId = dto.VendorId,
                Notes = dto.Notes,
                InvoiceDate = DateTime.UtcNow
            };

            decimal totalAmount = 0;

            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                if (itemDto.UnitCost < 0)
                    throw new Exception("Unit cost cannot be negative.");

                var part = await _context.Parts.FindAsync(itemDto.PartId);
                if (part == null)
                    throw new Exception($"Part with ID {itemDto.PartId} not found.");

                var invoiceItem = new PurchaseInvoiceItem
                {
                    PartId = part.Id,
                    Quantity = itemDto.Quantity,
                    UnitCost = itemDto.UnitCost
                };

                totalAmount += itemDto.UnitCost * itemDto.Quantity;

                // Update stock: Purchase increases stock quantity
                part.StockQuantity += itemDto.Quantity;

                invoice.Items.Add(invoiceItem);
            }

            invoice.TotalAmount = totalAmount;

            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}
