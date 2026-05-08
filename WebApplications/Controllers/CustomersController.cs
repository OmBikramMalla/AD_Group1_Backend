using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FullName))
                    return BadRequest(new { message = "Customer name is required." });

                if (string.IsNullOrWhiteSpace(dto.Email))
                    return BadRequest(new { message = "Email is required." });

                if (string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest(new { message = "Password is required." });

                if (string.IsNullOrWhiteSpace(dto.Phone))
                    return BadRequest(new { message = "Phone number is required." });

                if (string.IsNullOrWhiteSpace(dto.VehicleNumber))
                    return BadRequest(new { message = "Vehicle number is required." });

                var customer = await _customerService.RegisterCustomerWithVehicleAsync(dto);

                return Ok(new
                {
                    message = "Customer account, profile, and vehicle registered successfully.",
                    customerId = customer.Id,
                    customer.FullName,
                    customer.Phone,
                    customer.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Search query is required." });

            var customers = await _customerService.SearchCustomersAsync(query);

            return Ok(customers);
        }
    }
}