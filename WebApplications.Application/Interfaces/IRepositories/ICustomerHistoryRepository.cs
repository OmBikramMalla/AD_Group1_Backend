using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
	public interface ICustomerHistoryRepository
	{
		Task<Customer?> GetCustomerHistoryAsync(long customerId);
		Task<Customer?> GetCustomerHistoryByEmailAsync(string email);
	}
}