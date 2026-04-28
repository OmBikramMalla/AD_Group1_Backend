using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("api/staff/customers")]
    [ApiController]
    public class StaffCustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public StaffCustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/staff/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/staff/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDetails(long id)
        {
            var customer = await _customerService.GetCustomerDetailsWithHistoryAsync(id);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }
    }
}