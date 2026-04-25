using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDTO>> GetModulesAsync();
        Task<ModuleDTO?> GetModuleAsync(int id);
        Task CreateModuleAsync(ModuleDTO dto);
        Task UpdateModuleAsync(int id, ModuleDTO dto);
        Task DeleteModuleAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<ModuleDTO>> GetHighCreditAsync(int min);
    }
}
