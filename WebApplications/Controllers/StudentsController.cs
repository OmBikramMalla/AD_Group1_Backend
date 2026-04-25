using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _service.GetStudentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var data = await _service.GetStudentAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDTO dto)
        {
            await _service.CreateStudentAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudentDTO dto)
        {
            await _service.UpdateStudentAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteStudentAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _service.GetCountAsync());
        }
    }
}