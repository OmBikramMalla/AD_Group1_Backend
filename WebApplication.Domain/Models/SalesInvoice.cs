namespace WebApplications.Domain.Models
{
    public class SalesInvoice
    {
        public long Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<SalesInvoiceItem> Items { get; set; } = new List<SalesInvoiceItem>();
    }
}