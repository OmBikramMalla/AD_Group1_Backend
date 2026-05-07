using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Services
{
    public class CustomerHistoryService : ICustomerHistoryService
    {
        private readonly ICustomerHistoryRepository _customerHistoryRepository;

        public CustomerHistoryService(ICustomerHistoryRepository customerHistoryRepository)
        {
            _customerHistoryRepository = customerHistoryRepository;
        }

        public async Task<CustomerHistoryDto?> GetCustomerHistoryAsync(long userId)
        {
            var customer = await _customerHistoryRepository.GetCustomerHistoryAsync(userId);

            if (customer == null)
                return null;

            return MapToCustomerHistoryDto(customer);
        }

        private CustomerHistoryDto MapToCustomerHistoryDto(Customer customer)
        {
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
                        Description = a.Description,
                        Status = a.Status,

                        VehicleName = a.Vehicle != null
                            ? $"{a.Vehicle.VehicleBrand} {a.Vehicle.VehicleModel}"
                            : string.Empty,

                        VehicleNumber = a.Vehicle != null
                            ? a.Vehicle.VehicleNumber
                            : string.Empty
                    })
                    .ToList(),

                ReviewHistory = customer.ServiceReviews
                    .OrderByDescending(r => r.ReviewDate)
                    .Select(r => new ReviewHistoryDto
                    {
                        ReviewId = r.Id,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        ReviewDate = r.ReviewDate,

                        ServiceAppointmentId = r.ServiceAppointmentId,
                        ServiceType = r.ServiceAppointment != null
                            ? r.ServiceAppointment.ServiceType
                            : string.Empty,
                        AppointmentDate = r.ServiceAppointment != null
                            ? r.ServiceAppointment.AppointmentDate
                            : null
                    })
                    .ToList()
            };
        }
    }
}