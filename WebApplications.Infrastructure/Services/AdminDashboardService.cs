using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Infrastructure.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public AdminDashboardService(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AdminDashboardDto> GetDashboardAsync()
        {
            var totalSales = await _context.SalesInvoices.SumAsync(x => x.TotalAmount);
            var totalPurchases = await _context.PurchaseInvoices.SumAsync(x => x.TotalAmount);

            var staffUsers = await _userManager.GetUsersInRoleAsync("Staff");

            var salesMonthly = await _context.SalesInvoices
                .GroupBy(x => new { x.InvoiceDate.Year, x.InvoiceDate.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Sales = g.Sum(x => x.TotalAmount)
                })
                .ToListAsync();

            var purchasesMonthly = await _context.PurchaseInvoices
                .GroupBy(x => new { x.PurchaseDate.Year, x.PurchaseDate.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Purchases = g.Sum(x => x.TotalAmount)
                })
                .ToListAsync();

            var monthKeys = salesMonthly
                .Select(x => new { x.Year, x.Month })
                .Union(purchasesMonthly.Select(x => new { x.Year, x.Month }))
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .TakeLast(6)
                .ToList();

            var chart = monthKeys.Select(k =>
            {
                var sales = salesMonthly.FirstOrDefault(x => x.Year == k.Year && x.Month == k.Month)?.Sales ?? 0;
                var purchases = purchasesMonthly.FirstOrDefault(x => x.Year == k.Year && x.Month == k.Month)?.Purchases ?? 0;

                return new DashboardChartDto
                {
                    Label = $"{k.Year}-{k.Month:D2}",
                    Sales = sales,
                    Purchases = purchases,
                    Profit = sales - purchases
                };
            }).ToList();

            var lowStockParts = await _context.Parts
                .Where(p => p.StockQuantity < 10)
                .OrderBy(p => p.StockQuantity)
                .Select(p => new LowStockPartDto
                {
                    Id = p.Id,
                    PartName = p.PartName,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            var recentSales = await _context.SalesInvoices
                .OrderByDescending(x => x.InvoiceDate)
                .Take(5)
                .Select(x => new RecentActivityDto
                {
                    Type = "Sale",
                    Description = "Sales invoice created",
                    Amount = x.TotalAmount,
                    Date = x.InvoiceDate
                })
                .ToListAsync();

            var recentPurchases = await _context.PurchaseInvoices
                .OrderByDescending(x => x.PurchaseDate)
                .Take(5)
                .Select(x => new RecentActivityDto
                {
                    Type = "Purchase",
                    Description = "Purchase invoice recorded",
                    Amount = x.TotalAmount,
                    Date = x.PurchaseDate
                })
                .ToListAsync();

            var activities = recentSales
                .Concat(recentPurchases)
                .OrderByDescending(x => x.Date)
                .Take(6)
                .ToList();

            return new AdminDashboardDto
            {
                TotalSales = totalSales,
                TotalPurchases = totalPurchases,
                NetProfit = totalSales - totalPurchases,
                TotalParts = await _context.Parts.CountAsync(),
                LowStockCount = lowStockParts.Count,
                ActiveStaffCount = staffUsers.Count,
                VendorCount = await _context.Vendors.CountAsync(),
                FinancialChart = chart,
                LowStockParts = lowStockParts,
                RecentActivities = activities
            };
        }
    }
}