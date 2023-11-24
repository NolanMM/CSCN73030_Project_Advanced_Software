using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class TimeAnalysisService
    {
        private readonly Analysis_Report_Center _reportCenter;

        public TimeAnalysisService(Analysis_Report_Center reportCenter)
        {
            _reportCenter = reportCenter ?? throw new ArgumentNullException(nameof(reportCenter));
        }

        public Dictionary<string, int> ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return _reportCenter.SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .GroupBy(s => s.date.Month)
                .ToDictionary(grp => $"Month {grp.Key}", grp => grp.Count());
        }
    }
}
