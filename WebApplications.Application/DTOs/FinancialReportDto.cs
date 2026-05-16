namespace WebApplications.Application.DTOs
{
    public class FinancialReportDto
    {
        public string Label { get; set; } = string.Empty;
        public decimal TotalSales { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal NetProfit { get; set; }
        public int SalesInvoiceCount { get; set; }
        public int PurchaseInvoiceCount { get; set; }
    }
}