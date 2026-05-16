namespace WebApplications.Application.DTOs
{
    public class AdminNotificationDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Priority { get; set; } = "medium";
        public bool IsRead { get; set; } = false;
    }
}