using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface ISalesInvoiceService
    {
        Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto);
        Task<bool> SendInvoiceEmailAsync(long invoiceId);

        Task<object> GetRecentInvoicesAsync();
        Task<object> GetSalesSummaryAsync();
    }
}