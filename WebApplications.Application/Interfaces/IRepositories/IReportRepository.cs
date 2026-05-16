using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IReportRepository
    {
        Task<List<CustomerReportDto>> GetTopSpendersAsync();
        Task<List<CustomerReportDto>> GetFrequentCustomersAsync();
        Task<List<CustomerReportDto>> GetPendingPaymentsAsync();
    }
}