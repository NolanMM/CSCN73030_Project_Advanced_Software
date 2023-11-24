using System;
using System.Linq;

namespace Server_Side.Services
{
    public class TotalSalesService : Analysis_Service_Center
    {
        public TotalSalesService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return SalesTransactions
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .Sum(s => s.TransactionValue);
        }
    }
}
