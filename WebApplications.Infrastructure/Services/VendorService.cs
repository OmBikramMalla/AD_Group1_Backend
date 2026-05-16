using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<List<Vendor>> GetAllAsync()
        {
            return await _vendorRepository.GetAllAsync();
        }

        public async Task<Vendor?> GetByIdAsync(long id)
        {
            return await _vendorRepository.GetByIdAsync(id);
        }

        public async Task<Vendor> CreateAsync(CreateVendorDto dto)
        {
            return await _vendorRepository.CreateAsync(dto);
        }

        public async Task<Vendor?> UpdateAsync(long id, UpdateVendorDto dto)
        {
            return await _vendorRepository.UpdateAsync(id, dto);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _vendorRepository.DeleteAsync(id);
        }
    }
}