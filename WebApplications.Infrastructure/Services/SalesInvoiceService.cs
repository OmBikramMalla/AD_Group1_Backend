using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;
        private readonly IEmailService _emailService;

        public SalesInvoiceService(ISalesInvoiceRepository salesInvoiceRepository,IEmailService emailService)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
            _emailService = emailService;
        }


        public Task<SalesInvoice> CreateAsync(CreateSalesInvoiceDto dto)
        {
            return _salesInvoiceRepository.CreateAsync(dto);
        }

        public async Task<bool> SendInvoiceEmailAsync(long invoiceId)
        {
            var invoice = await _salesInvoiceRepository.GetInvoiceWithDetailsAsync(invoiceId);

            if (invoice == null)
                throw new Exception("Invoice not found.");

            if (invoice.Customer == null || string.IsNullOrWhiteSpace(invoice.Customer.Email))
                throw new Exception("Customer email not found.");

            var body = $"Dear {invoice.Customer.FullName},\n\n" +
                       $"Your sales invoice has been generated.\n\n" +
                       $"Invoice ID: {invoice.Id}\n" +
                       $"Invoice Date: {invoice.InvoiceDate}\n" +
                       $"Total Amount: Rs. {invoice.TotalAmount}\n" +
                       (invoice.DiscountAmount > 0 ? $"Loyalty Discount Applied: Rs. {invoice.DiscountAmount}\n" : "") +
                       $"Paid Amount: Rs. {invoice.PaidAmount}\n" +
                       $"Due Amount: Rs. {invoice.TotalAmount - invoice.PaidAmount}\n\n" +
                       $"Items:\n";

            foreach (var item in invoice.Items)
            {
                body += $"- {item.Part?.PartName} | Qty: {item.Quantity} | Unit Price: Rs. {item.UnitPrice}\n";
            }

            body += "\nThank you,\nVehicle Parts Center";

            await _emailService.SendEmailAsync(
                invoice.Customer.Email,
                "Your Sales Invoice",
                body
            );

            return true;
        }

        public Task<object> GetRecentInvoicesAsync()
        {
            return _salesInvoiceRepository.GetRecentInvoicesAsync();
        }

        public Task<object> GetSalesSummaryAsync()
        {
            return _salesInvoiceRepository.GetSalesSummaryAsync();
        }
        public Task<object> GetAllInvoicesAsync()
        {
            return _salesInvoiceRepository.GetAllInvoicesAsync();
        }
    }
}