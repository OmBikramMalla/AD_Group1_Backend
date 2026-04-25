using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetCoursesAsync();
        Task<CourseDTO?> GetCourseAsync(int id);
        Task CreateCourseAsync(CourseDTO dto);
        Task UpdateCourseAsync(int id, CourseDTO dto);
        Task DeleteCourseAsync(int id);

        Task<int> GetCountAsync();
        Task<int> GetTotalCreditsAsync();
    }
}
