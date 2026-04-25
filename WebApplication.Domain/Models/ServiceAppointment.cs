namespace WebApplications.Domain.Models
{
    public class ServiceAppointment
    {
        public long Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}