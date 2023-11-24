using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class PriceAnalysisService : Analysis_Report_Center
    {
        public PriceAnalysisService(Analysis_Report_Center reportCenter) : base(
            reportCenter.Valid_User_Views_Table,
            reportCenter.Website_logs_table,
            reportCenter.SalesTransactionsTable,
            reportCenter.FeedbackTable){
        }

        public object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            // Retrieve sales transactions within the specified date range
            var relevantSales = SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .ToList();

            // Calculate the average order value if there are any relevant sales
            decimal averageOrderValue = relevantSales.Any() ? relevantSales.Average(s => s.Order_Value) : 0;

            return averageOrderValue;
        }
    }
}
