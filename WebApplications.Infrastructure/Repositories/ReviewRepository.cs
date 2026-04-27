using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceReview> CreateAsync(ServiceReview review)
        {
            _context.ServiceReviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}