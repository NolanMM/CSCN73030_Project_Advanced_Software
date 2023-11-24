using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Linq;

namespace Server_Side.Services
{
    public class AverageOrderValueService
    {
        private Analysis_Report_Center _analysisReportCenter;

        public AverageOrderValueService(Analysis_Report_Center analysisReportCenter)
        {
            _analysisReportCenter = analysisReportCenter;
        }

        public decimal ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            if (_analysisReportCenter.SalesTransactionsTable == null)
                throw new InvalidOperationException("SalesTransactions data is not initialized.");

            var relevantSales = _analysisReportCenter.SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .ToList();

            // Calculate the average value if there are any relevant sales
            return relevantSales.Any() ? relevantSales.Average(s => s.Order_Value) : 0;
        }
    }
}
