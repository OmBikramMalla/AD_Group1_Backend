using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/admin/notifications")]
	[ApiController]
	public class AdminNotificationsController : ControllerBase
	{
		private readonly IAdminNotificationService _adminNotificationService;

		public AdminNotificationsController(IAdminNotificationService adminNotificationService)
		{
			_adminNotificationService = adminNotificationService;
		}

		[HttpGet]
		public async Task<IActionResult> GetNotifications()
		{
			var result = await _adminNotificationService.GetAllNotificationsAsync();
			return Ok(result);
		}

		[HttpPost("send-credit-reminders")]
		public async Task<IActionResult> SendCreditReminders()
		{
			var result = await _adminNotificationService.SendCreditRemindersAsync();
			return Ok(result);
		}
	}
}