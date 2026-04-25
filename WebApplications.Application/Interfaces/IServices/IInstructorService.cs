using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDTO>> GetAllAsync();
        Task<InstructorDTO?> GetByIdAsync(int id);
        Task CreateAsync(InstructorDTO dto);
        Task UpdateAsync(int id, InstructorDTO dto);
        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<int>> GetDistinctHireYearsAsync();
    }
}
