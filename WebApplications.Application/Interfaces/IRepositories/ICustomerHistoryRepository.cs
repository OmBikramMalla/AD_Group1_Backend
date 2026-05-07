using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface ICustomerHistoryRepository
    {
        Task<Customer?> GetCustomerHistoryAsync(long userId);
    }
}