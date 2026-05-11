using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IPurchaseInvoiceRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object?> GetByIdAsync(long id);
        Task<PurchaseInvoice> CreateAsync(CreatePurchaseInvoiceDto dto);
    }
}
