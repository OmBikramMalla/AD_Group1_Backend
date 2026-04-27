using WebApplications.Domain.Models;
using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(long id);
        Task<object?> GetCustomerDetailsWithHistoryAsync(long id);
        Task<Customer> RegisterCustomerWithVehicleAsync(RegisterCustomerDto dto);
    }
}