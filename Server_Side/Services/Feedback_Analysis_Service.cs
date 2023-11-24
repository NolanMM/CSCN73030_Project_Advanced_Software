using System;
using System.Linq;

namespace Server_Side.Services
{
    public class FeedbackAnalysisService : Analysis_Service_Center
    {
        public FeedbackAnalysisService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return Feedbacks
                .GroupBy(f => f.StarRating.ToString())
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }
    }
}
