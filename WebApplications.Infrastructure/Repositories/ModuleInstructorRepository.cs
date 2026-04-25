using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class ModuleInstructorRepository : IModuleInstructorRepository
    {
        private readonly AppDbContext _context;

        public ModuleInstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int moduleId, int instructorId)
        {
            return await _context.ModuleInstructors
                .AnyAsync(x => x.ModuleId == moduleId && x.InstructorId == instructorId);
        }

        public async Task AddAsync(ModuleInstructor model)
        {
            await _context.ModuleInstructors.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int moduleId, int instructorId)
        {
            var record = await _context.ModuleInstructors
                .FirstOrDefaultAsync(x => x.ModuleId == moduleId && x.InstructorId == instructorId);

            if (record != null)
            {
                _context.ModuleInstructors.Remove(record);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(List<ModuleInstructor> list)
        {
            await _context.ModuleInstructors.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetFullDetailsAsync()
        {
            return await _context.ModuleInstructors
                .Include(mi => mi.Module)
                .Include(mi => mi.Instructor)
                .Select(mi => new
                {
                    ModuleTitle = mi.Module.Title,
                    InstructorName = mi.Instructor.FirstName + " " + mi.Instructor.LastName
                })
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.ModuleInstructors.CountAsync();
        }

        public async Task<IEnumerable<object>> GetModuleCountPerInstructorAsync()
        {
            return await _context.ModuleInstructors
                .Include(mi => mi.Instructor)
                .GroupBy(mi => new
                {
                    mi.InstructorId,
                    mi.Instructor.FirstName,
                    mi.Instructor.LastName
                })
                .Select(g => new
                {
                    InstructorName = g.Key.FirstName + " " + g.Key.LastName,
                    ModuleCount = g.Count()
                })
                .ToListAsync();
        }
    }
}
