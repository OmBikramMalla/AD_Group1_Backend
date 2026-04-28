using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<CustomerReportDto>> GetTopSpendersAsync()
        {
            return await _reportRepository.GetTopSpendersAsync();
        }

        public async Task<List<CustomerReportDto>> GetFrequentCustomersAsync()
        {
            return await _reportRepository.GetFrequentCustomersAsync();
        }

        public async Task<List<CustomerReportDto>> GetPendingPaymentsAsync()
        {
            return await _reportRepository.GetPendingPaymentsAsync();
        }
    }
}