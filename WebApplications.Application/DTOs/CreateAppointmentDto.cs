namespace WebApplications.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public long VehicleId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}