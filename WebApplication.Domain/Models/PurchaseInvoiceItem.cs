namespace WebApplications.Domain.Models
{
    /// <summary>
    /// A single line item on a PurchaseInvoice — which part, how many, and at what cost.
    /// </summary>
    public class PurchaseInvoiceItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }

        // The purchase invoice this item belongs to
        public long PurchaseInvoiceId { get; set; }
        public PurchaseInvoice? PurchaseInvoice { get; set; }

        // The part being purchased/restocked
        public long PartId { get; set; }
        public Part? Part { get; set; }
    }
}
