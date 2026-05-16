using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IPartRequestRepository
    {
        Task<PartRequest> CreateForCustomerUserIdAsync(CreatePartRequestDto dto, long userId);
    }
}