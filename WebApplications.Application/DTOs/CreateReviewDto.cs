namespace WebApplications.Application.DTOs
{
    public class CreateReviewDto
    {
        public long ServiceAppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}