using System;
using System.Linq;

namespace Server_Side.Services
{
    public class AverageOrderValueService : Analysis_Service_Center
    {
        public AverageOrderValueService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            var salesInPeriod = SalesTransactions
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();

            if (!salesInPeriod.Any()) return 0;

            return salesInPeriod.Average(s => s.TransactionValue);
        }
    }
}
