using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IReviewRepository
    {
        Task<ServiceReview> CreateForCustomerUserIdAsync(CreateReviewDto dto, long userId);
    }
}