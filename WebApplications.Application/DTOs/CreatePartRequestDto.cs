namespace WebApplications.Application.DTOs
{
    public class CreatePartRequestDto
    {
        public long CustomerId { get; set; }
        public string RequestedPartName { get; set; } = string.Empty;
        public string VehicleInfo { get; set; } = string.Empty;
    }
}