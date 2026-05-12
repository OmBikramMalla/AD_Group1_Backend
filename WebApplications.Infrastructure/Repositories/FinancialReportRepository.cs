using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Repositories
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly AppDbContext _context;

        public FinancialReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FinancialReportDto>> GetFinancialReportsAsync(string type)
        {
            var sales = await _context.SalesInvoices
                .Select(s => new
                {
                    Date = s.InvoiceDate,
                    Amount = s.TotalAmount
                })
                .ToListAsync();

            var purchases = await _context.PurchaseInvoices
                .Select(p => new
                {
                    Date = p.PurchaseDate,
                    Amount = p.TotalAmount
                })
                .ToListAsync();

            var salesGrouped = sales
                .GroupBy(s => GetLabel(s.Date, type))
                .Select(g => new
                {
                    Label = g.Key,
                    TotalSales = g.Sum(x => x.Amount),
                    SalesInvoiceCount = g.Count()
                })
                .ToList();

            var purchaseGrouped = purchases
                .GroupBy(p => GetLabel(p.Date, type))
                .Select(g => new
                {
                    Label = g.Key,
                    TotalPurchases = g.Sum(x => x.Amount),
                    PurchaseInvoiceCount = g.Count()
                })
                .ToList();

            var labels = salesGrouped.Select(s => s.Label)
                .Union(purchaseGrouped.Select(p => p.Label))
                .ToList();

            var result = labels.Select(label =>
            {
                var sale = salesGrouped.FirstOrDefault(s => s.Label == label);
                var purchase = purchaseGrouped.FirstOrDefault(p => p.Label == label);

                var totalSales = sale?.TotalSales ?? 0;
                var totalPurchases = purchase?.TotalPurchases ?? 0;

                return new FinancialReportDto
                {
                    Label = label,
                    TotalSales = totalSales,
                    TotalPurchases = totalPurchases,
                    NetProfit = totalSales - totalPurchases,
                    SalesInvoiceCount = sale?.SalesInvoiceCount ?? 0,
                    PurchaseInvoiceCount = purchase?.PurchaseInvoiceCount ?? 0
                };
            })
            .OrderBy(r => r.Label)
            .ToList();

            return result;
        }

        private static string GetLabel(DateTime date, string type)
        {
            return type switch
            {
                "daily" => date.ToString("yyyy-MM-dd"),
                "monthly" => date.ToString("yyyy-MM"),
                "yearly" => date.ToString("yyyy"),
                _ => date.ToString("yyyy-MM-dd")
            };
        }
    }
}