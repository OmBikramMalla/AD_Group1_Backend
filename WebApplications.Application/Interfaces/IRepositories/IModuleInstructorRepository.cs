using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IModuleInstructorRepository
    {
        Task<bool> ExistsAsync(int moduleId, int instructorId);
        Task AddAsync(ModuleInstructor model);
        Task RemoveAsync(int moduleId, int instructorId);
        Task AddRangeAsync(List<ModuleInstructor> list);
        Task<IEnumerable<object>> GetFullDetailsAsync();
        Task<int> CountAsync();
        Task<IEnumerable<object>> GetModuleCountPerInstructorAsync();
    }
}
