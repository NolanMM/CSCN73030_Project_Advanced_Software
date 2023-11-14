using System;

namespace Server_Side.Services
{
    public class BestCategoryAnalysisService : Analysis_Service_Center
    {
        public BestCategoryAnalysisService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {   //placeholder values
            return new Dictionary<string, decimal>
            {
                {"Category1", 1234.56M},
                {"Category2", 7890.12M},
            };
        }
    }
}
