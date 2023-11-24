using System;
using System.Linq;

namespace Server_Side.Services
{
    public class TimeAnalysisService : Analysis_Service_Center
    {
        public TimeAnalysisService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return SalesTransactions
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .GroupBy(s => s.Date.Month)
                .ToDictionary(grp => $"Month {grp.Key}", grp => grp.Count());
        }
    }
}
