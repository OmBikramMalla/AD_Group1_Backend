using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleInstructorsController : ControllerBase
    {
        private readonly IModuleInstructorService _service;

        public ModuleInstructorsController(IModuleInstructorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Assign(ModuleInstructorDTO dto)
        {
            var result = await _service.AssignAsync(dto);

            if (result.Contains("already"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int moduleId, int instructorId)
        {
            await _service.RemoveAsync(moduleId, instructorId);
            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAssign(List<ModuleInstructorDTO> list)
        {
            await _service.BulkAssignAsync(list);
            return Ok("Bulk assigned");
        }

        [HttpGet("full-details")]
        public async Task<IActionResult> GetFullDetails()
        {
            return Ok(await _service.GetFullDetailsAsync());
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _service.CountAsync());
        }

        [HttpGet("module-count")]
        public async Task<IActionResult> ModuleCountPerInstructor()
        {
            return Ok(await _service.GetModuleCountPerInstructorAsync());
        }
    }
}