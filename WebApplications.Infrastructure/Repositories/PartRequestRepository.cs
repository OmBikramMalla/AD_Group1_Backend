using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class PartRequestRepository : IPartRequestRepository
    {
        private readonly AppDbContext _context;

        public PartRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PartRequest> CreateForCustomerUserIdAsync(CreatePartRequestDto dto, long userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                throw new Exception("Customer not found for logged-in user.");

            var request = new PartRequest
            {
                CustomerId = customer.Id,
                RequestedPartName = dto.RequestedPartName,
                VehicleInfo = dto.VehicleInfo,
                Status = "Pending"
            };

            _context.PartRequests.Add(request);
            await _context.SaveChangesAsync();

            return request;
        }
    }
}