namespace WebApplications.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public long CustomerId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
    }
}