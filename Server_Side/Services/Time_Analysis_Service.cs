using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class TimeAnalysisService : Analysis_Report_Center
    {
        public TimeAnalysisService(Analysis_Report_Center reportCenter)
            : base(reportCenter.SalesTransactionsTable, reportCenter.Website_logs_table, reportCenter.FeedbackTable, reportCenter.Valid_User_Views_Table)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .GroupBy(s => s.date.Month)
                .ToDictionary(grp => $"Month {grp.Key}", grp => grp.Count());
        }
    }
}
