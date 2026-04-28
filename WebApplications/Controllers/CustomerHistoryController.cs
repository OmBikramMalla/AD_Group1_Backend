using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
	[Authorize(Roles = "Customer")]
	[Route("api/customers")]
	[ApiController]
	public class CustomerHistoryController : ControllerBase
	{
		private readonly ICustomerHistoryService _customerHistoryService;

		public CustomerHistoryController(ICustomerHistoryService customerHistoryService)
		{
			_customerHistoryService = customerHistoryService;
		}

		[HttpGet("my-history")]
		public async Task<IActionResult> GetMyHistory()
		{
			var email = User.FindFirst(ClaimTypes.Email)?.Value;

			if (string.IsNullOrWhiteSpace(email))
			{
				return Unauthorized(new { message = "Invalid token." });
			}

			var result = await _customerHistoryService.GetCustomerHistoryByEmailAsync(email);

			if (result == null)
			{
				return NotFound(new { message = "Customer history not found." });
			}

			return Ok(result);
		}
	}
}