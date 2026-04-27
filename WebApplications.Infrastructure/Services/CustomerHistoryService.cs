using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Infrastructure.Services
{
	public class CustomerHistoryService : ICustomerHistoryService
	{
		private readonly ICustomerHistoryRepository _customerHistoryRepository;

		public CustomerHistoryService(ICustomerHistoryRepository customerHistoryRepository)
		{
			_customerHistoryRepository = customerHistoryRepository;
		}

		public async Task<CustomerHistoryDto?> GetCustomerHistoryAsync(long customerId)
		{
			var customer = await _customerHistoryRepository.GetCustomerHistoryAsync(customerId);

			if (customer == null)
			{
				return null;
			}

			return new CustomerHistoryDto
			{
				CustomerId = customer.Id,
				FullName = customer.FullName,
				Phone = customer.Phone,
				Email = customer.Email,

				PurchaseHistory = customer.SalesInvoices
					.OrderByDescending(i => i.InvoiceDate)
					.Select(i => new PurchaseHistoryDto
					{
						InvoiceId = i.Id,
						InvoiceDate = i.InvoiceDate,
						TotalAmount = i.TotalAmount,
						PaidAmount = i.PaidAmount,
						DueAmount = i.TotalAmount - i.PaidAmount
					})
					.ToList(),

				ServiceHistory = customer.ServiceAppointments
					.OrderByDescending(a => a.AppointmentDate)
					.Select(a => new ServiceHistoryDto
					{
						AppointmentId = a.Id,
						AppointmentDate = a.AppointmentDate,
						ServiceType = a.ServiceType,
						Status = a.Status
					})
					.ToList(),

				ReviewHistory = customer.ServiceReviews
					.OrderByDescending(r => r.ReviewDate)
					.Select(r => new ReviewHistoryDto
					{
						ReviewId = r.Id,
						Rating = r.Rating,
						Comment = r.Comment,
						ReviewDate = r.ReviewDate
					})
					.ToList()
			};
		}
	}
}