using System;
using System.Linq;

namespace Server_Side.Services
{
    public class ConversionRateService : Analysis_Service_Center
    {
        public ConversionRateService(List<UserView> userViews, List<PageView> pageViews, List<SaleTransaction> salesTransactions, List<Feedback> feedbacks)
            : base(userViews, pageViews, salesTransactions, feedbacks)
        {
        }

        public override object ExecuteAnalysis(DateTime startDate, DateTime endDate)
        {
            // Requires productId to be specified
            string productId = "specific-product-id";
            int productPageViews = PageViews.Count(p => p.ProductId == productId && p.DateTime >= startDate && p.DateTime <= endDate);
            int productSales = SalesTransactions.Count(s => s.Date >= startDate && s.Date <= endDate);

            if (productPageViews == 0) return 0;

            return (decimal)productSales / productPageViews;
        }
    }
}
