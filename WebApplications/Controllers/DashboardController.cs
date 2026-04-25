using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/dashboard
        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();
            var totalModules = await _context.Modules.CountAsync();
            var totalEnrollments = await _context.Enrollments.CountAsync();

            var result = new
            {
                Students = totalStudents,
                Courses = totalCourses,
                Modules = totalModules,
                Enrollments = totalEnrollments
            };

            return Ok(result);
        }
    }
}