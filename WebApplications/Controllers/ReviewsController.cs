using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public ReviewsController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            var result = await _customerService.CreateReviewAsync(dto);

            return Ok(new
            {
                message = "Review submitted",
                review = result
            });
        }
    }
}