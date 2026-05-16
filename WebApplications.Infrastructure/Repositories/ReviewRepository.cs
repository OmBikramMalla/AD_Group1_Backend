using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
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

        public async Task<ServiceReview> CreateForCustomerUserIdAsync(CreateReviewDto dto, long userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                throw new Exception("Customer not found for logged-in user.");

            var appointment = await _context.ServiceAppointments
                .FirstOrDefaultAsync(a =>
                    a.Id == dto.ServiceAppointmentId &&
                    a.CustomerId == customer.Id);

            if (appointment == null)
                throw new ArgumentException("Selected appointment was not found for this customer.");

            if (appointment.Status != "Completed")
                throw new ArgumentException("You can only review completed appointments.");

            var existingReview = await _context.ServiceReviews
                .AnyAsync(r => r.ServiceAppointmentId == appointment.Id);

            if (existingReview)
                throw new ArgumentException("You have already submitted a review for this appointment.");

            var review = new ServiceReview
            {
                CustomerId = customer.Id,
                ServiceAppointmentId = appointment.Id,
                Rating = dto.Rating,
                Comment = dto.Comment,
                ReviewDate = DateTime.UtcNow
            };

            _context.ServiceReviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }
    }
}