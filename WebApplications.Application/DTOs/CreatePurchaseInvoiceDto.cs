namespace WebApplications.Application.DTOs
{
    public class CreatePurchaseInvoiceDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public long VendorId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<CreatePurchaseInvoiceItemDto> Items { get; set; } = new();
    }

    public class CreatePurchaseInvoiceItemDto
    {
        public long PartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}