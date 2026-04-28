using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;

        public SalesInvoiceService(ISalesInvoiceRepository salesInvoiceRepository)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
        }

        public Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto)
        {
            return _salesInvoiceRepository.CreateAsync(dto);
        }
    }
}