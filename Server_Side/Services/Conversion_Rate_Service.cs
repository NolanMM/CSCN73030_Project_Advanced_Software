using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class ConversionRateService : Analysis_Report_Center
    {
        public ConversionRateService() : base()
        {
        }

        public Dictionary<string, decimal> ExecuteAnalysis(DateTime startDate, DateTime endDate, string productId)
        {
            if (SalesTransactionsTable == null || Website_logs_table == null)
                throw new InvalidOperationException("SalesTransactionsTable or Website_logs_table data is not initialized.");

            var productPageViews = Website_logs_table
                .Count(p => p.Product_ID == productId && p.Start_Time >= startDate && p.Start_Time <= endDate);

            //var productSales = SalesTransactionsTable //SaleTransaction class doesn't have a Product_ID field, we can't directly link transactions to products. To calculate the conversion rate, we need to track which products were purchased in each transaction
                //.Count(s => s.Product_ID == productId && s.date >= startDate && s.date <= endDate);

            if (productPageViews == 0)
                return new Dictionary<string, decimal>();

            return new Dictionary<string, decimal>
            {
                //{ productId, (decimal)productSales / productPageViews }
            };
        }
    }
}
