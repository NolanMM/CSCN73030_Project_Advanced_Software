using System;
using System.Linq;

namespace Server_Side.Services
{
    public class PageViewsService : Analysis_Service_Center
    {
        public PageViewsService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            return PageViews
                .Where(p => p.DateTime >= startDate && p.DateTime <= endDate)
                .GroupBy(p => p.PageUrl)
                .ToDictionary(grp => grp.Key, grp => grp.Count());
        }
    }
}
