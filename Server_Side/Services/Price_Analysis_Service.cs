using System;

namespace Server_Side.Services
{
    public class PriceAnalysisService : Analysis_Service_Center
    {
        public PriceAnalysisService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            // Mock implementation: Requires product prices
            return new Dictionary<string, decimal>
            {
                {"Product1", 99.99M},
                {"Product2", 49.99M},
            };
        }
