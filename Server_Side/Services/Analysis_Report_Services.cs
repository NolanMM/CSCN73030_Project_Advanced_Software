using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class Analysis_Report_Services
    {
        // Private inner data classes

        private class UserView
        {
            public string UserId { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private class PageView
        {
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public string PageUrl { get; set; }
            public string PageInfo { get; set; }
            public string ProductId { get; set; }
            public DateTime DateTime { get; set; }
        }

        private class SaleTransaction
        {
            public string TransactionId { get; set; }
            public string UserId { get; set; }
            public decimal TransactionValue { get; set; }
            public DateTime Date { get; set; }
        }

        private class Feedback
        {
            public string UserId { get; set; }
            public string ProductId { get; set; }
            public int StarRating { get; set; }
        }

        // Private storage for the data classes

        private List<UserView> Valid_User_Views_Table = new List<UserView>();
        private List<PageView> Website_logs_table = new List<PageView>();
        private List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        private List<Feedback> FeedbackTable = new List<Feedback>();

        // Public service methods

        public int GetUniqueVisitors(DateTime startDate, DateTime endDate)
        {
            return Valid_User_Views_Table
                .Where(v => v.Timestamp >= startDate && v.Timestamp <= endDate)
                .Select(v => v.UserId)
                .Distinct()
                .Count();
        }

        public Dictionary<string, int> GetPageViews(DateTime startDate, DateTime endDate)
        {
            return Website_logs_table
                .Where(p => p.DateTime >= startDate && p.DateTime <= endDate)
                .GroupBy(p => p.PageUrl)
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }

        public decimal GetTotalSales(DateTime startDate, DateTime endDate)
        {
            return SalesTransactionsTable
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .Sum(s => s.TransactionValue);
        }

        public decimal GetAverageOrderValue(DateTime startDate, DateTime endDate)
        {
            var salesInPeriod = SalesTransactionsTable
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();

            if (!salesInPeriod.Any()) return 0;

            return salesInPeriod.Average(s => s.TransactionValue);
        }

        public Dictionary<string, int> GetFeedbackAnalysis()
        {
            return FeedbackTable
                .GroupBy(f => f.StarRating.ToString())
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }

        public decimal GetConversionRate(string productId, DateTime startDate, DateTime endDate)
        {
            int productPageViews = Website_logs_table.Count(p => p.ProductId == productId && p.DateTime >= startDate && p.DateTime <= endDate);
            int productSales = SalesTransactionsTable.Count(s => s.Date >= startDate && s.Date <= endDate); 

            if (productPageViews == 0) return 0;

            return (decimal)productSales / productPageViews;
        }

        public Dictionary<string, decimal> GetBestCategoryAnalysis()
        {
            // Mock implementation: Requires Product-Category mapping
            return new Dictionary<string, decimal>
            {
                {"Category1", 1234.56M},
                {"Category2", 7890.12M},
                // ... other categories
            };
        }

        public Dictionary<string, int> GetTimeAnalysis()
        {
            return SalesTransactionsTable
                .GroupBy(s => s.Date.Month)
                .ToDictionary(grp => $"Month {grp.Key}", grp => grp.Count());
        }

        public Dictionary<string, decimal> GetPriceAnalysis()
        {
            // Mock implementation: Requires product prices
            return new Dictionary<string, decimal>
            {
                {"Product1", 99.99M},
                {"Product2", 49.99M},
                // ... other products
            };
        }

        public Dictionary<string, int> GetSalesAnalysis()
        {
            // Mock implementation: Sales by product
            return new Dictionary<string, int>
            {
                {"Product1", 1000},
                {"Product2", 500},
                // ... other products
            };
        }

        public decimal GetProfitAndLoss()
        {
            // Mock implementation: Calculate profit and loss
            return SalesTransactionsTable.Sum(s => s.TransactionValue) - 10000;  // 10000 is a mock operational cost
        }
    }
}