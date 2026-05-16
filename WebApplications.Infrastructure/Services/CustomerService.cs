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

        public Task<object> GetAllCustomersAsync()
        {
            return _customerRepository.GetAllCustomersAsync();
        }

        public Task<Customer?> GetCustomerByIdAsync(long id)
        {
            return _customerRepository.GetCustomerByIdAsync(id);
        }

        public Task<object?> GetCustomerDetailsWithHistoryAsync(long id)
        {
            return _customerRepository.GetCustomerDetailsWithHistoryAsync(id);
        }

        public Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto)
        {
            return _customerRepository.RegisterCustomerWithVehicleAsync(dto);
        }

        public Task<object> SearchCustomersAsync(string query)
        {
            return _customerRepository.SearchCustomersAsync(query);
        }

        public Task<object?> GetMyProfileAsync(long userId)
        {
            return _customerRepository.GetMyProfileAsync(userId);
        }

        public Task<object?> UpdateMyProfileAsync(long userId, UpdateCustomerProfileDto dto)
        {
            return _customerRepository.UpdateMyProfileAsync(userId, dto);
        }

        public Task<Vehicle> AddMyVehicleAsync(long userId, VehicleDto dto)
        {
            return _customerRepository.AddMyVehicleAsync(userId, dto);
        }

        public Task<Vehicle?> UpdateMyVehicleAsync(long userId, long vehicleId, VehicleDto dto)
        {
            return _customerRepository.UpdateMyVehicleAsync(userId, vehicleId, dto);
        }

        public async Task<bool> DeleteMyVehicleAsync(long userId, long vehicleId)
        {
            return await _customerRepository.DeleteMyVehicleAsync(userId, vehicleId);
        }
    }
}