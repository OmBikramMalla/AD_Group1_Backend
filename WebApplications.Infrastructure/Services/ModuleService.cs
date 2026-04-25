using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repo;

        public ModuleService(IModuleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ModuleDTO>> GetModulesAsync()
        {
            var modules = await _repo.GetAllAsync();

            return modules.Select(m => new ModuleDTO
            {
                Id = m.Id,
                Title = m.Title,
                Credtis = m.Credtis,
                CourseId = m.CourseId
            });
        }

        public async Task<ModuleDTO?> GetModuleAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null) return null;

            return new ModuleDTO
            {
                Id = m.Id,
                Title = m.Title,
                Credtis = m.Credtis,
                CourseId = m.CourseId
            };
        }

        public async Task CreateModuleAsync(ModuleDTO dto)
        {
            var module = new Module
            {
                Title = dto.Title,
                Credtis = dto.Credtis,
                CourseId = dto.CourseId
            };

            await _repo.AddAsync(module);
        }

        public async Task UpdateModuleAsync(int id, ModuleDTO dto)
        {
            var module = await _repo.GetByIdAsync(id);
            if (module == null) return;

            module.Title = dto.Title;
            module.Credtis = dto.Credtis;
            module.CourseId = dto.CourseId;

            await _repo.UpdateAsync(module);
        }

        public async Task DeleteModuleAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repo.GetCountAsync();
        }

        public async Task<IEnumerable<ModuleDTO>> GetHighCreditAsync(int min)
        {
            var modules = await _repo.GetHighCreditAsync(min);

            return modules.Select(m => new ModuleDTO
            {
                Id = m.Id,
                Title = m.Title,
                Credtis = m.Credtis,
                CourseId = m.CourseId
            });
        }
    }
}
