using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class UserView
    {
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class PageView
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string PageUrl { get; set; }
        public string PageInfo { get; set; }
        public string ProductId { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class SaleTransaction
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public decimal TransactionValue { get; set; }
        public DateTime Date { get; set; }
    }

    public class Feedback
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int StarRating { get; set; }
    }

    public class AnalyticsService
    {
        private List<UserView> Valid_User_Views_Table = new List<UserView>();
        private List<PageView> Website_logs_table = new List<PageView>();
        private List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        private List<Feedback> FeedbackTable = new List<Feedback>();

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
                .Where(v => v.DateTime >= startDate && v.DateTime <= endDate)
                .GroupBy(v => v.PageUrl)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public decimal GetTotalSales(DateTime startDate, DateTime endDate)
        {
            return SalesTransactionsTable
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .Sum(s => s.TransactionValue);
        }

        public decimal GetAverageOrderValue(DateTime startDate, DateTime endDate)
        {
            var salesInRange = SalesTransactionsTable
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();

            var totalValue = salesInRange.Sum(s => s.TransactionValue);
            var totalCount = salesInRange.Count;

            return totalCount != 0 ? totalValue / totalCount : 0;
        }

        public Dictionary<string, int> GetFeedbackAnalysis()
        {
            return FeedbackTable
                .GroupBy(f => f.ProductId)
                .ToDictionary(group => group.Key, group => group.ToList().Sum(g => g.StarRating) / group.Count());
        }

        public decimal GetConversionRate(string productId, DateTime startDate, DateTime endDate)
        {
            var productPageViews = Website_logs_table
                .Where(v => v.DateTime >= startDate && v.DateTime <= endDate && v.ProductId == productId)
                .Count();

            var productSales = SalesTransactionsTable
                .Where(s => s.Date >= startDate && s.Date <= endDate && s.TransactionValue == decimal.Parse(productId))
                .Count();

            return productPageViews != 0 ? (decimal)productSales / productPageViews : 0;
        }

    }
}
