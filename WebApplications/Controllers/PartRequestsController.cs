using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/part-requests")]
    [ApiController]
    public class PartRequestsController : ControllerBase
    {
        private readonly IPartRequestService _partRequestService;

        public PartRequestsController(IPartRequestService partRequestService)
        {
            _partRequestService = partRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartRequest(CreatePartRequestDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized(new { message = "Invalid token." });

            var userId = long.Parse(userIdClaim);

            var result = await _partRequestService.CreatePartRequestForUserAsync(dto, userId);

            return Ok(new
            {
                message = "Part request created successfully",
                request = result
            });
        }
    }
}