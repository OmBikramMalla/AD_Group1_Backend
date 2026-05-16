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
                .Include(pi => pi.Items)
                .OrderByDescending(pi => pi.PurchaseDate)
                .Select(pi => new
                {
                    pi.Id,
                    pi.InvoiceNumber,
                    pi.PurchaseDate,
                    pi.TotalAmount,
                    pi.Notes,
                    VendorName = pi.Vendor != null ? pi.Vendor.VendorName : "Unknown",
                    ItemCount = pi.Items.Sum(i => i.Quantity)
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
                    pi.InvoiceNumber,
                    pi.PurchaseDate,
                    pi.TotalAmount,
                    pi.Notes,
                    Vendor = pi.Vendor,
                    Items = pi.Items.Select(i => new
                    {
                        i.Id,
                        i.PartId,
                        PartName = i.Part != null ? i.Part.PartName : "Unknown",
                        i.Quantity,
                        i.UnitCost,
                        LineTotal = i.Quantity * i.UnitCost
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

            if (string.IsNullOrWhiteSpace(dto.InvoiceNumber))
                throw new Exception("Invoice number is required.");

            var duplicateInvoice = await _context.PurchaseInvoices
                .AnyAsync(pi => pi.InvoiceNumber == dto.InvoiceNumber);

            if (duplicateInvoice)
                throw new Exception("Invoice number already exists.");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var invoice = new PurchaseInvoice
                {
                    InvoiceNumber = dto.InvoiceNumber,
                    VendorId = dto.VendorId,
                    PurchaseDate = dto.PurchaseDate == default ? DateTime.UtcNow : dto.PurchaseDate,
                    InvoiceDate = DateTime.UtcNow,
                    Notes = dto.Notes
                };

                decimal totalAmount = 0;

                foreach (var itemDto in dto.Items)
                {
                    if (itemDto.PartId <= 0)
                        throw new Exception("Valid part is required.");

                    if (itemDto.Quantity <= 0)
                        throw new Exception("Quantity must be greater than zero.");

                    if (itemDto.UnitCost <= 0)
                        throw new Exception("Unit cost must be greater than zero.");

                    var part = await _context.Parts.FindAsync(itemDto.PartId);
                    if (part == null)
                        throw new Exception($"Part with ID {itemDto.PartId} not found.");

                    var lineTotal = itemDto.UnitCost * itemDto.Quantity;
                    totalAmount += lineTotal;

                    part.StockQuantity += itemDto.Quantity;

                    invoice.Items.Add(new PurchaseInvoiceItem
                    {
                        PartId = part.Id,
                        Quantity = itemDto.Quantity,
                        UnitCost = itemDto.UnitCost
                    });
                }

                invoice.TotalAmount = totalAmount;

                _context.PurchaseInvoices.Add(invoice);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return invoice;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}