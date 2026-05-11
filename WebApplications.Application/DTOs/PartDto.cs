namespace WebApplications.Application.DTOs
{
    public class CreatePartDto
    {
        public string PartName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class UpdatePartDto
    {
        public string PartName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
