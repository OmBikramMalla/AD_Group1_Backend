using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppDbContext _context;

        public VendorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vendor>> GetAllAsync()
        {
            return await _context.Vendors
                .OrderBy(v => v.Id)
                .ToListAsync();
        }

        public async Task<Vendor?> GetByIdAsync(long id)
        {
            return await _context.Vendors.FindAsync(id);
        }

        public async Task<Vendor> CreateAsync(CreateVendorDto dto)
        {
            var vendor = new Vendor
            {
                VendorName = dto.VendorName,
                ContactPerson = dto.ContactPerson,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return vendor;
        }

        public async Task<Vendor?> UpdateAsync(long id, UpdateVendorDto dto)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
                return null;

            vendor.VendorName = dto.VendorName;
            vendor.ContactPerson = dto.ContactPerson;
            vendor.Phone = dto.Phone;
            vendor.Email = dto.Email;
            vendor.Address = dto.Address;

            await _context.SaveChangesAsync();

            return vendor;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
                return false;

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}