using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;

        public EnrollmentService(IEnrollmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EnrollmentDTO>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();

            return data.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate
            });
        }

        public async Task CreateAsync(EnrollmentDTO dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate
            };

            await _repo.AddAsync(enrollment);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repo.GetCountAsync();
        }

        public async Task<IEnumerable<EnrollmentDTO>> GetByDateAsync(DateTime date)
        {
            var data = await _repo.GetByDateAsync(date);

            return data.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate
            });
        }

        public async Task<IEnumerable<object>> GetFullDetailsAsync()
        {
            return await _repo.GetFullDetailsAsync();
        }
    }
}
