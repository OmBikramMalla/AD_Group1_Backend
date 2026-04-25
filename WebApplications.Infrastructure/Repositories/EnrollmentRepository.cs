using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Enrollments.CountAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByDateAsync(DateTime date)
        {
            return await _context.Enrollments
                .Where(e => e.EnrollmentDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFullDetailsAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new
                {
                    e.Id,
                    StudentName = e.Student.FirstName + " " + e.Student.LastName,
                    CourseName = e.Course.Name,
                    e.EnrollmentDate
                })
                .ToListAsync();
        }
    }
}
