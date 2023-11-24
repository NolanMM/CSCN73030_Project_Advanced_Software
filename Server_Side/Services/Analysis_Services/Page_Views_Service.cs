using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services.Analysis_Services
{
    public class PageViewsService : Analysis_Report_Center
    {
        public PageViewsService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
            // Additional initialization, if needed
        }

        public Dictionary<string, int> ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            var relevantPageViews = Website_logs_table
                .Where(pv => pv.Start_Time >= startDate && pv.Start_Time <= endDate)
                .ToList();

            var pageViewsByPageName = relevantPageViews
                .GroupBy(pv => pv.Page_Name)
                .ToDictionary(group => group.Key, group => group.Count());

            return pageViewsByPageName;
        }
    }
}
