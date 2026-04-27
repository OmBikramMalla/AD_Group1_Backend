using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IReviewService
    {
        Task<ServiceReview> CreateReviewAsync(CreateReviewDto dto);
    }
}