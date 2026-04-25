using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module?> GetByIdAsync(int id);
        Task AddAsync(Module module);
        Task UpdateAsync(Module module);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<Module>> GetHighCreditAsync(int min);
    }
}
