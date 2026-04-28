using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(long id);
        Task<object?> GetCustomerDetailsWithHistoryAsync(long id);
        Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto);
        Task<object> SearchCustomersAsync(string query);
        Task<object?> GetMyProfileAsync(long userId);
        Task<object?> UpdateMyProfileAsync(long userId, UpdateCustomerProfileDto dto);
        Task<Vehicle> AddMyVehicleAsync(long userId, VehicleDto dto);
        Task<Vehicle?> UpdateMyVehicleAsync(long userId, long vehicleId, VehicleDto dto);
    }
}