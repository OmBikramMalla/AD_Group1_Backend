using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesAsync()
        {
            var courses = await _repo.GetAllAsync();

            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Name = c.Name,
                DurationYears = c.DurationYears
            });
        }

        public async Task<CourseDTO?> GetCourseAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            return new CourseDTO
            {
                Id = c.Id,
                Name = c.Name,
                DurationYears = c.DurationYears
            };
        }

        public async Task CreateCourseAsync(CourseDTO dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                DurationYears = dto.DurationYears
            };

            await _repo.AddAsync(course);
        }

        public async Task UpdateCourseAsync(int id, CourseDTO dto)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course == null) return;

            course.Name = dto.Name;
            course.DurationYears = dto.DurationYears;

            await _repo.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repo.GetCountAsync();
        }

        public async Task<int> GetTotalCreditsAsync()
        {
            return await _repo.GetTotalCreditsAsync();
        }
    }
}
