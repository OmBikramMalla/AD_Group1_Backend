using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _service.GetCoursesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var data = await _service.GetCourseAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDTO dto)
        {
            await _service.CreateCourseAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CourseDTO dto)
        {
            await _service.UpdateCourseAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCourseAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _service.GetCountAsync());
        }

        [HttpGet("total-credits")]
        public async Task<IActionResult> TotalCredits()
        {
            return Ok(await _service.GetTotalCreditsAsync());
        }
    }
}