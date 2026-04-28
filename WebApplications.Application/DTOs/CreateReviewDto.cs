namespace WebApplications.Application.DTOs
{
    public class CreateReviewDto
    {
        public long CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}