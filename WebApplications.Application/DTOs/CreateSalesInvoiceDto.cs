namespace WebApplications.Application.DTOs
{
    public class CreateSalesInvoiceDto
    {
        public long CustomerId { get; set; }
        public decimal PaidAmount { get; set; }
        public List<CreateSalesInvoiceItemDto> Items { get; set; } = new();
    }

    public class CreateSalesInvoiceItemDto
    {
        public long PartId { get; set; }
        public int Quantity { get; set; }
    }
}