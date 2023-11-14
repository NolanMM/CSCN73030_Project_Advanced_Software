using System;
using System.Linq;

namespace Server_Side.Services
{
    public class UniqueVisitorsService : Analysis_Service_Center
    {
        public UniqueVisitorsService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return UserViews
                .Where(v => v.Timestamp >= startDate && v.Timestamp <= endDate)
                .Select(v => v.UserId)
                .Distinct()
                .Count();
        }
    }
}
