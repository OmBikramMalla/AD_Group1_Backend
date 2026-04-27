using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/part-requests")]
    [ApiController]
    public class PartRequestsController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public PartRequestsController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartRequest(CreatePartRequestDto dto)
        {
            var result = await _customerService.CreatePartRequestAsync(dto);

            return Ok(new
            {
                message = "Part request created",
                request = result
            });
        }
    }
}