namespace WebApplications.Domain.Models
{
    public class Part
    {
        public long Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}