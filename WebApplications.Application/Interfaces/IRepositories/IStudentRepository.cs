using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
    }
}
