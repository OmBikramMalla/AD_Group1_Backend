using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
	[Route("api/customers")]
	[ApiController]
	public class CustomerHistoryController : ControllerBase
	{
		private readonly ICustomerHistoryService _customerHistoryService;

		public CustomerHistoryController(ICustomerHistoryService customerHistoryService)
		{
			_customerHistoryService = customerHistoryService;
		}

		[HttpGet("{id}/history")]
		public async Task<IActionResult> GetCustomerHistory(long id)
		{
			var result = await _customerHistoryService.GetCustomerHistoryAsync(id);

			if (result == null)
			{
				return NotFound(new
				{
					message = "Customer not found"
				});
			}

			return Ok(result);
		}
	}
}