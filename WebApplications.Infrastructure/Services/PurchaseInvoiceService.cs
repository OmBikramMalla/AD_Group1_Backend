using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

        public PurchaseInvoiceService(IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
        }

        public Task<IEnumerable<object>> GetAllAsync()
        {
            return _purchaseInvoiceRepository.GetAllAsync();
        }

        public Task<object?> GetByIdAsync(long id)
        {
            return _purchaseInvoiceRepository.GetByIdAsync(id);
        }

        public Task<PurchaseInvoice> CreateAsync(CreatePurchaseInvoiceDto dto)
        {
            return _purchaseInvoiceRepository.CreateAsync(dto);
        }
    }
}