using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class PartRequestService : IPartRequestService
    {
        private readonly IPartRequestRepository _partRequestRepository;

        public PartRequestService(IPartRequestRepository partRequestRepository)
        {
            _partRequestRepository = partRequestRepository;
        }

        public async Task<PartRequest> CreatePartRequestForUserAsync(CreatePartRequestDto dto, long userId)
        {
            return await _partRequestRepository.CreateForCustomerUserIdAsync(dto, userId);
        }
    }
}