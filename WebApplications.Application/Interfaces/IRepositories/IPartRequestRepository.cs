using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IPartRequestRepository
    {
        Task<PartRequest> CreateAsync(PartRequest partRequest);
    }
}