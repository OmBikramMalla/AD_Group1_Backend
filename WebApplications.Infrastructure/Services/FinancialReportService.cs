using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Infrastructure.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;

        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FinancialReportDto>> GetFinancialReportsAsync(string type)
        {
            type = type.ToLower();

            if (type != "daily" && type != "monthly" && type != "yearly")
                throw new Exception("Invalid report type. Use daily, monthly, or yearly.");

            return await _repository.GetFinancialReportsAsync(type);
        }
    }
}