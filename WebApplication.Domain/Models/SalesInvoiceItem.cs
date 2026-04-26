namespace WebApplications.Domain.Models
{
    public class SalesInvoiceItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public long SalesInvoiceId { get; set; }
        public SalesInvoice? SalesInvoice { get; set; }

        public long PartId { get; set; }
        public Part? Part { get; set; }
    }
}