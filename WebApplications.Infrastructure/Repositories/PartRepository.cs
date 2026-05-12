using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly AppDbContext _context;

        public PartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await _context.Parts
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Part?> GetByIdAsync(long id)
        {
            return await _context.Parts.FindAsync(id);
        }

        public async Task<Part> CreateAsync(CreatePartDto dto)
        {
            var part = new Part
            {
                PartName = dto.PartName,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<Part?> UpdateAsync(long id, UpdatePartDto dto)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null) return null;

            part.PartName = dto.PartName;
            part.Price = dto.Price;
            part.StockQuantity = dto.StockQuantity;

            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null) return false;

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Part>> GetLowStockPartsAsync(int threshold)
        {
            return await _context.Parts
                .Where(p => p.StockQuantity < threshold)
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();
        }
    }
}
