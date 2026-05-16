namespace WebApplications.Application.DTOs
{
    public class AdminDashboardDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal NetProfit { get; set; }

        public int TotalParts { get; set; }
        public int LowStockCount { get; set; }
        public int ActiveStaffCount { get; set; }
        public int VendorCount { get; set; }

        public List<DashboardChartDto> FinancialChart { get; set; } = new();
        public List<LowStockPartDto> LowStockParts { get; set; } = new();
        public List<RecentActivityDto> RecentActivities { get; set; } = new();
    }

    public class DashboardChartDto
    {
        public string Label { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public decimal Purchases { get; set; }
        public decimal Profit { get; set; }
    }

    public class LowStockPartDto
    {
        public long Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }

    public class RecentActivityDto
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}