using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Infrastructure.Presistance;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;

        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllAsync()
        {
            return await _context.Modules.ToListAsync();
        }

        public async Task<Module?> GetByIdAsync(int id)
        {
            return await _context.Modules.FindAsync(id);
        }

        public async Task AddAsync(Module module)
        {
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Module module)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                // remove related instructors
                var relations = _context.ModuleInstructors.Where(mi => mi.ModuleId == id);
                _context.ModuleInstructors.RemoveRange(relations);

                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Modules.CountAsync();
        }

        public async Task<IEnumerable<Module>> GetHighCreditAsync(int min)
        {
            return await _context.Modules
                .Where(m => m.Credtis > min)
                .ToListAsync();
        }
    }
}
