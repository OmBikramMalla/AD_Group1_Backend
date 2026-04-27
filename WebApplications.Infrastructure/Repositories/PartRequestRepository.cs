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

        public async Task<PartRequest> CreateAsync(PartRequest partRequest)
        {
            _context.PartRequests.Add(partRequest);
            await _context.SaveChangesAsync();
            return partRequest;
        }
    }
}