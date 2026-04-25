using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDTO>> GetAllAsync();
        Task CreateAsync(EnrollmentDTO dto);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<EnrollmentDTO>> GetByDateAsync(DateTime date);
        Task<IEnumerable<object>> GetFullDetailsAsync();
    }
}
