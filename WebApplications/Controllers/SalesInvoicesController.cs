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
    }
}