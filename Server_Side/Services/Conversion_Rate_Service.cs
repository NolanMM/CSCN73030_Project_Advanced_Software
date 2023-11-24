using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class ConversionRateService : Analysis_Report_Center
    {
        public ConversionRateService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public Dictionary<string, decimal> ExecuteAnalysis(DateTime startDate, DateTime endDate, string productId)
        {
            if (SalesTransactionsTable == null || Website_logs_table == null)
                throw new InvalidOperationException("SalesTransactionsTable or Website_logs_table data is not initialized.");//catch exception

            var productPageViews = Website_logs_table
                .Where(p => p.Product_ID == productId && p.Start_Time >= startDate && p.Start_Time <= endDate)
                .Count();

            var productSales = SalesTransactionsTable
                .Where(s => s.Product_ID == productId && s.date >= startDate && s.date <= endDate)
                .Count();

            if (productPageViews == 0)
                return new Dictionary<string, decimal> { { productId, 0 } };

            decimal conversionRate = (decimal)productSales / productPageViews;

            return new Dictionary<string, decimal>
            {
                { productId, conversionRate }
            };
        }
    }
}
