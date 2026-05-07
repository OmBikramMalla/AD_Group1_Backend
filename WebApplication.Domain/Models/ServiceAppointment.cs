using WebApplications.Domain.Models;

namespace WebApplications.Domain.Models
{
    public class ServiceAppointment
    {
        public long Id { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public long VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public ICollection<ServiceReview> Reviews { get; set; } = new List<ServiceReview>();
    }
}