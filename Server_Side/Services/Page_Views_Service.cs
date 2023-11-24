using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class PageViewsService : Analysis_Report_Center
    {
        public PageViewsService()
        {
            // Base constructor
        }

        public Dictionary<string, int> ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            // Filter and group PageView data based on your analysis criteria (e.g., date range)
            var relevantPageViews = Website_logs_table
                .Where(pv => pv.Start_Time >= startDate && pv.Start_Time <= endDate)
                .ToList();

            // Group the relevant PageView data by Page Name and count the occurrences
            var pageViewsByPageName = relevantPageViews
                .GroupBy(pv => pv.Page_Name)
                .ToDictionary(group => group.Key, group => group.Count());

            return pageViewsByPageName;
        }
    }
}
