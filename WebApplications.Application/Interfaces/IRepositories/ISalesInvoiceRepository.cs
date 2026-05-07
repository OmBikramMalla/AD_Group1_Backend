using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface ISalesInvoiceRepository
    {
        Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto);
        Task<SalesInvoice?> GetInvoiceWithDetailsAsync(long id);

        Task<object> GetRecentInvoicesAsync();
        Task<object> GetSalesSummaryAsync();
    }
}