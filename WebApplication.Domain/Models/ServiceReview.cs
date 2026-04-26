namespace WebApplications.Domain.Models
{
    public class ServiceReview
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}