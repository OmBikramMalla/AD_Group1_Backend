using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IReviewRepository
    {
        Task<ServiceReview> CreateAsync(ServiceReview review);
    }
}