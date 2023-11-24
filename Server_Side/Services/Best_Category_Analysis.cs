using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class BestCategoryAnalysisService
    {
        private Analysis_Report_Center _analysisReportCenter;

        public BestCategoryAnalysisService(Analysis_Report_Center analysisReportCenter)
        {
            _analysisReportCenter = analysisReportCenter;
        }

        public Dictionary<string, decimal> ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            if (_analysisReportCenter.SalesTransactionsTable == null)
                throw new InvalidOperationException("SalesTransactions data is not initialized.");

            return _analysisReportCenter.SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .GroupBy(s => s.User_ID) // Grouping by User_ID 
                .Select(group => new { UserID = group.Key, TotalSales = group.Sum(s => s.Order_Value) })
                .OrderByDescending(result => result.TotalSales)
                .ToDictionary(result => result.UserID, result => result.TotalSales);
        }
    }
}
