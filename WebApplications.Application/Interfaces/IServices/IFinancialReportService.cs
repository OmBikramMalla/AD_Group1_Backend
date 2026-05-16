using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IFinancialReportService
    {
        Task<List<FinancialReportDto>> GetFinancialReportsAsync(string type);
    }
}