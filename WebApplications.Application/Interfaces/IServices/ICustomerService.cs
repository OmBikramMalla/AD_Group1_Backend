using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(long id);
        Task<object?> GetCustomerDetailsWithHistoryAsync(long id);
    }
}