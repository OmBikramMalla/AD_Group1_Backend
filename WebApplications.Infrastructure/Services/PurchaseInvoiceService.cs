using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IPurchaseInvoiceRepository _repository;

        public PurchaseInvoiceService(IPurchaseInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<object?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<PurchaseInvoice> CreateAsync(CreatePurchaseInvoiceDto dto)
        {
            return await _repository.CreateAsync(dto);
        }
    }
}
