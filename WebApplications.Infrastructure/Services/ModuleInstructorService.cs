using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class ModuleInstructorService : IModuleInstructorService
    {
        private readonly IModuleInstructorRepository _repo;

        public ModuleInstructorService(IModuleInstructorRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AssignAsync(ModuleInstructorDTO dto)
        {
            var exists = await _repo.ExistsAsync(dto.ModuleId, dto.InstructorId);

            if (exists)
                return "Instructor already assigned";

            var model = new ModuleInstructor
            {
                ModuleId = dto.ModuleId,
                InstructorId = dto.InstructorId
            };

            await _repo.AddAsync(model);
            return "Assigned successfully";
        }

        public async Task RemoveAsync(int moduleId, int instructorId)
        {
            await _repo.RemoveAsync(moduleId, instructorId);
        }

        public async Task BulkAssignAsync(List<ModuleInstructorDTO> list)
        {
            var data = list.Select(x => new ModuleInstructor
            {
                ModuleId = x.ModuleId,
                InstructorId = x.InstructorId
            }).ToList();

            await _repo.AddRangeAsync(data);
        }

        public async Task<IEnumerable<object>> GetFullDetailsAsync()
        {
            return await _repo.GetFullDetailsAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _repo.CountAsync();
        }

        public async Task<IEnumerable<object>> GetModuleCountPerInstructorAsync()
        {
            return await _repo.GetModuleCountPerInstructorAsync();
        }
    }
}
