namespace WebApplications.Domain.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string VehicleNumber { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleBrand { get; set; } = string.Empty;

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}