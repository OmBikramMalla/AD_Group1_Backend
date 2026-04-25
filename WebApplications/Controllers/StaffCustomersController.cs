using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/staff/customers")]
    [ApiController]
    public class StaffCustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public StaffCustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDetails(long id)
        {
            var result = await _customerService.GetCustomerDetailsWithHistoryAsync(id);

            if (result == null)
                return NotFound("Customer not found");

            return Ok(result);
        }
    }
}