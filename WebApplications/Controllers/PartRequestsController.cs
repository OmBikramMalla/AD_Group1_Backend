using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;

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
            var result = await _partRequestService.CreatePartRequestAsync(dto);

            return Ok(new
            {
                message = "Part request created",
                request = result
            });
        }
    }
}