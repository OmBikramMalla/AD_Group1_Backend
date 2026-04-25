using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetStudentsAsync();
        Task<StudentDTO?> GetStudentAsync(int id);
        Task CreateStudentAsync(StudentDTO dto);
        Task UpdateStudentAsync(int id, StudentDTO dto);
        Task DeleteStudentAsync(int id);

        Task<int> GetCountAsync();
    }
}
