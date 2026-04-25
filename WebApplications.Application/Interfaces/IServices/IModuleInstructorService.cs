using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IModuleInstructorService
    {
        Task<string> AssignAsync(ModuleInstructorDTO dto);
        Task RemoveAsync(int moduleId, int instructorId);
        Task BulkAssignAsync(List<ModuleInstructorDTO> list);
        Task<IEnumerable<object>> GetFullDetailsAsync();
        Task<int> CountAsync();
        Task<IEnumerable<object>> GetModuleCountPerInstructorAsync();
    }
}
