using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(long id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<object?> GetCustomerDetailsWithHistoryAsync(long id)
        {
            return await _customerRepository.GetCustomerDetailsWithHistoryAsync(id);
        }

        public async Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto)
        {
            return await _customerRepository.RegisterCustomerWithVehicleAsync(dto);
        }

        public async Task<ServiceAppointment> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            return await _customerRepository.CreateAppointmentAsync(dto);
        }
        public async Task<PartRequest> CreatePartRequestAsync(CreatePartRequestDto dto)
        {
            return await _customerRepository.CreatePartRequestAsync(dto);
        }
    }
}