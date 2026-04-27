using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IPartRequestService
    {
        Task<PartRequest> CreatePartRequestAsync(CreatePartRequestDto dto);
    }
}