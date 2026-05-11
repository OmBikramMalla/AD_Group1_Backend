using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IPartService
    {
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part?> GetByIdAsync(long id);
        Task<Part> CreateAsync(CreatePartDto dto);
        Task<Part?> UpdateAsync(long id, UpdatePartDto dto);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Part>> GetLowStockPartsAsync(int threshold = 10);
    }
}
