using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/customer-profile")]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerProfileController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        private long GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new Exception("User ID not found in token.");

            return long.Parse(claim.Value);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _customerService.GetMyProfileAsync(GetUserId());

            if (result == null)
                return NotFound(new { message = "Customer profile not found." });

            return Ok(result);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile(UpdateCustomerProfileDto dto)
        {
            var result = await _customerService.UpdateMyProfileAsync(GetUserId(), dto);

            if (result == null)
                return NotFound(new { message = "Customer profile not found." });

            return Ok(new
            {
                message = "Profile updated successfully.",
                profile = result
            });
        }

        [HttpPost("vehicles")]
        public async Task<IActionResult> AddVehicle(VehicleDto dto)
        {
            try
            {
                var result = await _customerService.AddMyVehicleAsync(GetUserId(), dto);

                return Ok(new
                {
                    message = "Vehicle added successfully.",
                    vehicle = new
                    {
                        result.Id,
                        result.VehicleNumber,
                        result.VehicleModel,
                        result.VehicleBrand
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("vehicles/{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(long vehicleId, VehicleDto dto)
        {
            try
            {
                var result = await _customerService.UpdateMyVehicleAsync(GetUserId(), vehicleId, dto);

                if (result == null)
                    return NotFound(new { message = "Vehicle not found." });

                return Ok(new
                {
                    message = "Vehicle updated successfully.",
                    vehicle = new
                    {
                        result.Id,
                        result.VehicleNumber,
                        result.VehicleModel,
                        result.VehicleBrand
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}