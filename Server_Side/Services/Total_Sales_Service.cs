using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services
{
    public class TotalSalesService : Analysis_Report_Center
    {
        public TotalSalesService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .Sum(s => s.Order_Value);
        }
    }
}
