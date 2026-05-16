using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IReviewService
    {
        Task<ServiceReview> CreateReviewForUserAsync(CreateReviewDto dto, long userId);
    }
}