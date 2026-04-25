using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _service;

        public ModulesController(IModuleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            return Ok(await _service.GetModulesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModule(int id)
        {
            var data = await _service.GetModuleAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModuleDTO dto)
        {
            await _service.CreateModuleAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ModuleDTO dto)
        {
            await _service.UpdateModuleAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteModuleAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _service.GetCountAsync());
        }

        [HttpGet("high-credit")]
        public async Task<IActionResult> HighCredit(int min)
        {
            return Ok(await _service.GetHighCreditAsync(min));
        }
    }
}