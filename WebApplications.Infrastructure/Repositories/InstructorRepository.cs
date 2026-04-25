using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<Instructor?> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Instructors.CountAsync();
        }

        public async Task<IEnumerable<int>> GetDistinctHireYearsAsync()
        {
            return await _context.Instructors
                .Select(i => i.HireDate.Year)
                .Distinct()
                .ToListAsync();
        }
    }
}
