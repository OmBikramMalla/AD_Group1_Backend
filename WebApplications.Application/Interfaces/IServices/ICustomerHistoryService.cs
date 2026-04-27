using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
	public interface ICustomerHistoryService
	{
		Task<CustomerHistoryDto?> GetCustomerHistoryAsync(long customerId);
	}
}