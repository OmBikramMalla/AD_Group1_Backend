using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task AddAsync(Instructor instructor);
        Task UpdateAsync(Instructor instructor);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<int>> GetDistinctHireYearsAsync();
    }
}
