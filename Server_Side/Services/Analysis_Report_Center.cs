using System;
using System.Collections.Generic;

namespace Server_Side.Services
{
    public abstract class Analysis_Service_Center
    {
        protected List<UserView> UserViews;
        protected List<PageView> PageViews;
        protected List<SaleTransaction> SalesTransactions;
        protected List<Feedback> Feedbacks;

        public Analysis_Service_Center(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
        {
            UserViews = userViews;
            PageViews = pageViews;
            SalesTransactions = salesTransactions;
            Feedbacks = feedbacks;
        }

        public abstract object ExecuteAnalysis(DateTime startDate, DateTime endDate);
    }
}
