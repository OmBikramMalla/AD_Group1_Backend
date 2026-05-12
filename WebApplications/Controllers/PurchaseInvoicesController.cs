using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/purchase-invoices")]
    [ApiController]
    public class PurchaseInvoicesController : ControllerBase
    {
        private readonly IPurchaseInvoiceService _service;

        public PurchaseInvoicesController(IPurchaseInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _service.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var invoice = await _service.GetByIdAsync(id);
            if (invoice == null)
                return NotFound(new { message = "Purchase invoice not found." });

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseInvoiceDto dto)
        {
            try
            {
                var invoice = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, new
                {
                    message = "Purchase invoice created successfully and stock updated.",
                    invoiceId = invoice.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
