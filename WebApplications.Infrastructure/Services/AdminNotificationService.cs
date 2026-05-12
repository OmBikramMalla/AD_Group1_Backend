using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Infrastructure.Services
{
    public class AdminNotificationService : IAdminNotificationService
    {
        private readonly IAdminNotificationRepository _adminNotificationRepository;
        private readonly IEmailService _emailService;

        public AdminNotificationService(
            IAdminNotificationRepository adminNotificationRepository,
            IEmailService emailService)
        {
            _adminNotificationRepository = adminNotificationRepository;
            _emailService = emailService;
        }

        public async Task<List<AdminNotificationDto>> GetAllNotificationsAsync()
        {
            var lowStock = await _adminNotificationRepository.GetLowStockNotificationsAsync();
            var overdueCredits = await _adminNotificationRepository.GetOverdueCreditNotificationsAsync();

            return lowStock
                .Concat(overdueCredits)
                .OrderByDescending(n => n.Priority == "high")
                .ToList();
        }

        public async Task<object> SendCreditRemindersAsync()
        {
            var overdueInvoices = await _adminNotificationRepository.GetOverdueCreditInvoicesAsync();

            foreach (var invoice in overdueInvoices)
            {
                var dueAmount = invoice.TotalAmount - invoice.PaidAmount;

                var body =
                    $"Dear {invoice.Customer!.FullName},\n\n" +
                    $"This is a reminder that your invoice #{invoice.Id} has an unpaid balance overdue for more than one month.\n\n" +
                    $"Invoice Date: {invoice.InvoiceDate}\n" +
                    $"Total Amount: Rs. {invoice.TotalAmount}\n" +
                    $"Paid Amount: Rs. {invoice.PaidAmount}\n" +
                    $"Due Amount: Rs. {dueAmount}\n\n" +
                    $"Please clear your pending credit as soon as possible.\n\n" +
                    $"Thank you,\nVehicle Parts Center";

                await _emailService.SendEmailAsync(
                    invoice.Customer.Email,
                    "Overdue Credit Payment Reminder",
                    body
                );
            }

            return new
            {
                message = "Credit reminder emails sent successfully.",
                count = overdueInvoices.Count
            };
        }
    }
}