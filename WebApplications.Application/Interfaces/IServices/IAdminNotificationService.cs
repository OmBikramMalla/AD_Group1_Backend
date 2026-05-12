using WebApplications.Application.DTOs;

namespace WebApplications.Application.Interfaces.IServices
{
    public interface IAdminNotificationService
    {
        Task<List<AdminNotificationDto>> GetAllNotificationsAsync();
        Task<object> SendCreditRemindersAsync();
    }
}