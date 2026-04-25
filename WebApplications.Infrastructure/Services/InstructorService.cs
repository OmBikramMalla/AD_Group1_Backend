using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<InstructorDTO>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();

            return data.Select(i => new InstructorDTO
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Email = i.Email,
                HireDate = i.HireDate
            });
        }

        public async Task<InstructorDTO?> GetByIdAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) return null;

            return new InstructorDTO
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Email = i.Email,
                HireDate = i.HireDate
            };
        }

        public async Task CreateAsync(InstructorDTO dto)
        {
            var instructor = new Instructor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                HireDate = dto.HireDate
            };

            await _repo.AddAsync(instructor);
        }

        public async Task UpdateAsync(int id, InstructorDTO dto)
        {
            var instructor = await _repo.GetByIdAsync(id);
            if (instructor == null) return;

            instructor.FirstName = dto.FirstName;
            instructor.LastName = dto.LastName;
            instructor.Email = dto.Email;
            instructor.HireDate = dto.HireDate;

            await _repo.UpdateAsync(instructor);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repo.GetCountAsync();
        }

        public async Task<IEnumerable<int>> GetDistinctHireYearsAsync()
        {
            return await _repo.GetDistinctHireYearsAsync();
        }
    }
}
