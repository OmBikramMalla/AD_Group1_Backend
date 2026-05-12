namespace WebApplications.Domain.Models
{
    /// <summary>
    /// Represents a purchase invoice created by Admin when buying parts from a vendor.
    /// When created, the quantity purchased is automatically added to the Part's stock.
    /// </summary>
    public class PurchaseInvoice
    {
        public long Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        // Vendor this purchase was made from
        public long VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        // Line items on this purchase invoice
        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
