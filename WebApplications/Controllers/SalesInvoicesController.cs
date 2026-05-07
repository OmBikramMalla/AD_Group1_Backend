using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("api/sales-invoices")]
    [ApiController]
    public class SalesInvoicesController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesInvoiceService;

        public SalesInvoicesController(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalesInvoiceDto dto)
        {
            try
            {
                var invoice = await _salesInvoiceService.CreateAsync(dto);

                return Ok(new
                {
                    message = "Sales invoice created successfully.",
                    invoiceId = invoice.Id,
                    invoice.TotalAmount,
                    invoice.PaidAmount,
                    dueAmount = invoice.TotalAmount - invoice.PaidAmount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("{id}/send-email")]
        public async Task<IActionResult> SendEmail(long id)
        {
            try
            {
                await _salesInvoiceService.SendInvoiceEmailAsync(id);

                return Ok(new
                {
                    message = "Invoice email sent successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentInvoices()
        {
            var result = await _salesInvoiceService.GetRecentInvoicesAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSalesSummary()
        {
            var result = await _salesInvoiceService.GetSalesSummaryAsync();
            return Ok(result);
        }
    }
}