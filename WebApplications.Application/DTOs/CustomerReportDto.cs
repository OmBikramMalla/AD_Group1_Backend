namespace WebApplications.Application.DTOs
{
    public class CustomerReportDto
    {
        public long CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;

        public decimal TotalSpent { get; set; }
        public int TotalAppointments { get; set; }
        public decimal PendingAmount { get; set; }
    }
}