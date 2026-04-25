using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task AddAsync(Enrollment enrollment);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<Enrollment>> GetByDateAsync(DateTime date);
        Task<IEnumerable<object>> GetFullDetailsAsync();
    }
}
