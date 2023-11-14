﻿namespace Server_Side.Services
{
    using Server_Side.DatabaseServices;
    using Server_Side.DatabaseServices.Services.Models.Interfaces;

    public class Analysis_Report_Services
    {
        private readonly List<UserView> Valid_User_Views_Table = new List<UserView>();
        private readonly List<PageView> Website_logs_table = new List<PageView>();
        private readonly List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        private readonly List<Feedback> FeedbackTable = new List<Feedback>();
        private string Services_Types { get; set; } = "Retrieve Data";

        public class UserView
        {
            public string User_Id { get; set; }
            public DateTime Timestamp { get; set; }
            public DateOnly End_Date { get; set; }
            public DateOnly Start_Date { get; set; }
        }

        public class PageView
        {
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public string PageUrl { get; set; }
            public string PageInfo { get; set; }
            public string ProductId { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime Start_Time { get; set; }
            public DateTime End_Time { get; set; }
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
            public int FeedbackId { get; set; }
            public string UserId { get; set; }
            public string ProductId { get; set; }
            public decimal StarRating { get; set; }
        }

        public async Task<bool> InitializeData(int Table_Number)
        {
            try
            {
                Services_Types = "Retrieve All Data";
                List<Group_1_Record_Abstraction>? databaseControl = await Database_Centre.GetDataForDatabaseServiceID(Table_Number);
                if (databaseControl != null)
                {
                    switch (Table_Number)
                    {
                        case 0:
                            Valid_User_Views_Table.AddRange(databaseControl.OfType<UserView>());
                            break;
                        case 1:
                            Website_logs_table.AddRange(databaseControl.OfType<PageView>());
                            break;
                        case 2:
                            SalesTransactionsTable.AddRange(databaseControl.OfType<SaleTransaction>());
                            break;
                        case 3:
                            FeedbackTable.AddRange(databaseControl.OfType<Feedback>());
                            break;
                        default:
                            Console.WriteLine($"Invalid table number: {Table_Number}");
                            return false;
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data for table {Table_Number}");
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Public service methods

        public int GetUniqueVisitors(DateTime startDate, DateTime endDate)
        {
            return Valid_User_Views_Table
                .Where(v => v.Timestamp >= startDate && v.Timestamp <= endDate)
                .Select(v => v.User_Id)
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