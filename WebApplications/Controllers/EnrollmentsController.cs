using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentDTO dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _service.GetCountAsync());
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            return Ok(await _service.GetByDateAsync(date));
        }

        [HttpGet("full-details")]
        public async Task<IActionResult> FullDetails()
        {
            return Ok(await _service.GetFullDetailsAsync());
        }
    }
}