using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/staff/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("top-spenders")]
        public async Task<IActionResult> GetTopSpenders()
        {
            var result = await _reportService.GetTopSpendersAsync();
            return Ok(result);
        }

        [HttpGet("frequent-customers")]
        public async Task<IActionResult> GetFrequentCustomers()
        {
            var result = await _reportService.GetFrequentCustomersAsync();
            return Ok(result);
        }

        [HttpGet("pending-payments")]
        public async Task<IActionResult> GetPendingPayments()
        {
            var result = await _reportService.GetPendingPaymentsAsync();
            return Ok(result);
        }
    }
}