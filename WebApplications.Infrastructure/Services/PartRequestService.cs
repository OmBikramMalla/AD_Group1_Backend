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

        public async Task<PartRequest> CreatePartRequestAsync(CreatePartRequestDto dto)
        {
            var partRequest = new PartRequest
            {
                CustomerId = dto.CustomerId,
                RequestedPartName = dto.RequestedPartName,
                VehicleInfo = dto.VehicleInfo,
                Status = "Pending"
            };

            return await _partRequestRepository.CreateAsync(partRequest);
        }
    }
}