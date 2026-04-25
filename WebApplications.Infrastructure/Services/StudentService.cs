using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StudentDTO>> GetStudentsAsync()
        {
            var students = await _repo.GetAllAsync();

            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            });
        }

        public async Task<StudentDTO?> GetStudentAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null) return null;

            return new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            };
        }

        public async Task CreateStudentAsync(StudentDTO dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            await _repo.AddAsync(student);
        }

        public async Task UpdateStudentAsync(int id, StudentDTO dto)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null) return;

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;

            await _repo.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repo.GetCountAsync();
        }
    }
}
