using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class FeedbackAnalysisService : Analysis_Report_Center
    {
        public FeedbackAnalysisService() : base()
        {
        }

        public Dictionary<string, int> ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            if (FeedbackTable == null)
                throw new InvalidOperationException("FeedbackTable data is not initialized.");

            return FeedbackTable
                .Where(f => f.Date_Updated >= startDate && f.Date_Updated <= endDate)
                .GroupBy(f => f.Stars_Rating.ToString())
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }
    }
}
