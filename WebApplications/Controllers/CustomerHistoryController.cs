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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized(new { message = "Invalid token." });

            var userId = long.Parse(userIdClaim);

            var result = await _customerHistoryService.GetCustomerHistoryAsync(userId);

            if (result == null)
                return NotFound(new { message = "Customer history not found." });

            return Ok(result);
        }
    }
}