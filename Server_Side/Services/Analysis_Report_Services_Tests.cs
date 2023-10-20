using Server_Side.Services;  // This imports the namespace from Analysis_Report_Services.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Tests
{
    public class AnalyticsServiceTests
    {
        private readonly AnalyticsService _service;

        public AnalyticsServiceTests()
        {
            _service = new AnalyticsService();

            // Populate _service with mock data for testing here. (You should populate all the lists)
        }

        public bool Test_GetUniqueVisitors()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            int uniqueVisitors = _service.GetUniqueVisitors(startDate, endDate);
            return uniqueVisitors >= 0; // adjust as per your expectations
        }

        public bool Test_GetPageViews()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            var pageViews = _service.GetPageViews(startDate, endDate);
            return pageViews != null && pageViews.Count > 0;
        }

        public bool Test_GetTotalSales()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal sales = _service.GetTotalSales(startDate, endDate);
            return sales >= 0;
        }

        public bool Test_GetAverageOrderValue()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal avgOrderValue = _service.GetAverageOrderValue(startDate, endDate);
            return avgOrderValue >= 0;
        }

        public bool Test_GetFeedbackAnalysis()
        {
            var feedbackAnalysis = _service.GetFeedbackAnalysis();
            return feedbackAnalysis != null && feedbackAnalysis.Count > 0;
        }

        public bool Test_GetConversionRate()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal conversionRate = _service.GetConversionRate("sampleProductId", startDate, endDate);
            return conversionRate >= 0 && conversionRate <= 1; // Should be between 0 and 1 (0% and 100%)
        }

        // Add other test methods for the rest of the public methods...

        public void RunAllTests()
        {
            Console.WriteLine($"Test_GetUniqueVisitors: {Test_GetUniqueVisitors()}");
            Console.WriteLine($"Test_GetPageViews: {Test_GetPageViews()}");
            Console.WriteLine($"Test_GetTotalSales: {Test_GetTotalSales()}");
            Console.WriteLine($"Test_GetAverageOrderValue: {Test_GetAverageOrderValue()}");
            Console.WriteLine($"Test_GetFeedbackAnalysis: {Test_GetFeedbackAnalysis()}");
            Console.WriteLine($"Test_GetConversionRate: {Test_GetConversionRate()}");
            // ... print results for other tests ...
        }
    }
}