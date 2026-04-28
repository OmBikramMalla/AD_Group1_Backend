using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IVendorService
    {
        Task<List<Vendor>> GetAllAsync();
        Task<Vendor?> GetByIdAsync(long id);
        Task<Vendor> CreateAsync(CreateVendorDto dto);
        Task<Vendor?> UpdateAsync(long id, UpdateVendorDto dto);
        Task<bool> DeleteAsync(long id);
    }
}