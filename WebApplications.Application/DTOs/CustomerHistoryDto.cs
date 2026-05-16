namespace WebApplications.Application.DTOs
{
    public class CustomerHistoryDto
    {
        public long CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<PurchaseHistoryDto> PurchaseHistory { get; set; } = new();
        public List<ServiceHistoryDto> ServiceHistory { get; set; } = new();
        public List<ReviewHistoryDto> ReviewHistory { get; set; } = new();
    }

    public class PurchaseHistoryDto
    {
        public long InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
    }

    public class ServiceHistoryDto
    {
        public long AppointmentId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string VehicleName { get; set; } = string.Empty;

        public string VehicleNumber { get; set; } = string.Empty;
    }

    public class ReviewHistoryDto
    {
        public long ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }

        public long ServiceAppointmentId { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public DateTime? AppointmentDate { get; set; }
    }
}