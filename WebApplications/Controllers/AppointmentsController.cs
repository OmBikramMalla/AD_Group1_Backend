using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized(new { message = "Invalid token." });

            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized(new { message = "Invalid user id in token." });

            try
            {
                var result = await _appointmentService.CreateAppointmentForUserAsync(dto, userId);

                return Ok(new
                {
                    message = "Appointment created successfully",
                    appointment = new
                    {
                        id = result.Id,
                        appointmentDate = result.AppointmentDate,
                        serviceType = result.ServiceType,
                        description = result.Description,
                        status = result.Status,
                        customerId = result.CustomerId,
                        vehicleId = result.VehicleId
                    }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
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

        [HttpGet("my")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized(new { message = "Invalid token." });

            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized(new { message = "Invalid user id in token." });

            var appointments = await _appointmentService.GetMyAppointmentsAsync(userId);

            return Ok(appointments.Select(a => new
            {
                id = a.Id,
                appointmentDate = a.AppointmentDate,
                serviceType = a.ServiceType,
                description = a.Description,
                status = a.Status,
                vehicleId = a.VehicleId
            }));
        }
    }
}