namespace WebApplications.Domain.Models
{
    public class PurchaseInvoice
    {
        public long Id { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public long VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}