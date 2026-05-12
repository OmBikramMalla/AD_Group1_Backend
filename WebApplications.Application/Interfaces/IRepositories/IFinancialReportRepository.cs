using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IFinancialReportRepository
    {
        Task<List<FinancialReportDto>> GetFinancialReportsAsync(string type);
    }
}