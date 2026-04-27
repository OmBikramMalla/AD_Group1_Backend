using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public AppointmentsController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var result = await _customerService.CreateAppointmentAsync(dto);

            return Ok(new
            {
                message = "Appointment created successfully",
                appointment = result
            });
        }
    }
}