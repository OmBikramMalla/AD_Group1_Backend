using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _repository;

        public PartService(IPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Part?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Part> CreateAsync(CreatePartDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<Part?> UpdateAsync(long id, UpdatePartDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Part>> GetLowStockPartsAsync(int threshold = 10)
        {
            return await _repository.GetLowStockPartsAsync(threshold);
        }
    }
}
