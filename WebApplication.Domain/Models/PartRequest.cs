namespace WebApplications.Domain.Models
{
    public class PartRequest
    {
        public long Id { get; set; }
        public string RequestedPartName { get; set; } = string.Empty;
        public string VehicleInfo { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}