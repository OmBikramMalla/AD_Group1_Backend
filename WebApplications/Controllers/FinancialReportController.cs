using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/admin/financial-reports")]
	[ApiController]
	public class FinancialReportsController : ControllerBase
	{
		private readonly IFinancialReportService _service;

		public FinancialReportsController(IFinancialReportService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetReports([FromQuery] string type = "monthly")
		{
			try
			{
				var result = await _service.GetFinancialReportsAsync(type);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}