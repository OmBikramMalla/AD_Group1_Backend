using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IReportService
    {
        Task<List<CustomerReportDto>> GetTopSpendersAsync();
        Task<List<CustomerReportDto>> GetFrequentCustomersAsync();
        Task<List<CustomerReportDto>> GetPendingPaymentsAsync();
    }
}