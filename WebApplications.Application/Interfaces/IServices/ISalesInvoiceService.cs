using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface ISalesInvoiceService
    {
        Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto);
    }
}