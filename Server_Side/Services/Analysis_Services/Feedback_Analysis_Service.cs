using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class FeedbackAnalysisService
    {
        private readonly Analysis_Report_Center _reportCenter;

        public FeedbackAnalysisService(Analysis_Report_Center reportCenter)
        {
            _reportCenter = reportCenter ?? throw new ArgumentNullException(nameof(reportCenter));
        }

        public async Task<Dictionary<string, int>> ExecuteAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            if (_reportCenter.FeedbackTable == null || !_reportCenter.FeedbackTable.Any())
                throw new InvalidOperationException("FeedbackTable data is not initialized.");

            return await Task.Run(() =>
            {
                return _reportCenter.FeedbackTable
                    .Where(f => f.Date_Updated >= startDate && f.Date_Updated <= endDate)
                    .GroupBy(f => f.Stars_Rating.ToString())
                    .ToDictionary(grp => grp.Key, grp => grp.Count());
            });
        }
    }
}
