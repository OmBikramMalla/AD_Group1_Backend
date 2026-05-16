using WebApplications.Application.DTOs;
using WebApplications.Domain.Models;

namespace WebApplications.Application.Interfaces.IRepositories
{
    public interface IAdminNotificationRepository
    {
        Task<List<AdminNotificationDto>> GetLowStockNotificationsAsync();
        Task<List<AdminNotificationDto>> GetOverdueCreditNotificationsAsync();
        Task<List<SalesInvoice>> GetOverdueCreditInvoicesAsync();
    }
}