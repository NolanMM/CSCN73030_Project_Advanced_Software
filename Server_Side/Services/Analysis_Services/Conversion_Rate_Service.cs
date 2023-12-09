using Newtonsoft.Json;
using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.Services.Analysis_Services
{
    public class ConversionRateService
    {
        public static async Task<int> Process(DateTime? startDate, DateTime? endDate, string? Product_ID)
        {
            if (startDate == null || endDate == null || Product_ID == null)
            {
                return 0;
            }
            var PageViewData = await Database_Centre.GetDataForDatabaseServiceID(1);
            var SalaTransactionData = await Database_Centre.GetDataForDatabaseServiceID(2);

            return ExecuteAnalysis(SalaTransactionData, PageViewData, startDate.Value, endDate.Value, Product_ID);
        }
        private static int ExecuteAnalysis(List<Group_1_Record_Abstraction>? SaleTransactionData, List<Group_1_Record_Abstraction>? PageViewData, DateTime startDate, DateTime endDate, string ProductID)
        {
            if (SaleTransactionData == null || PageViewData == null)
            {
                return 0;
            }

            var filteredSaleTransactions = SaleTransactionData
                .Where(transaction => transaction is SaleTransaction tr && tr.date >= startDate && tr.date <= endDate && tr.Details_Products.Contains(ProductID))
                .ToList();

            var filteredPageViews = PageViewData
                .Where(pageView => pageView is PageView Pv && Pv.Start_Time >= startDate && Pv.Start_Time <= endDate && Pv.Product_ID == ProductID)
                .ToList();

            int totalProductSalesQuantity = filteredSaleTransactions.Sum(transaction =>
            {
                if (transaction is SaleTransaction tr)
                {
                    return ParseProductDetails(tr.Details_Products)
                        .Where(product => product.Product_ID == ProductID)
                        .Sum(product => product.Product_Quantity);
                }
                return 0;
            });

            int totalPageViewsQuantity = filteredPageViews.Count;

            int ConversionRate = totalPageViewsQuantity != 0 ? totalProductSalesQuantity / totalPageViewsQuantity : 0;

            return ConversionRate;
        }

        private static List<ProductDetails> ParseProductDetails(string detailsProducts)
        {
            // Replace single quotes with double quotes
            detailsProducts = detailsProducts.Replace("'", "\"");

            // Deserialize the modified JSON string
            var results = JsonConvert.DeserializeObject<List<ProductDetails>>(detailsProducts);
            if(results == null) { results = new List<ProductDetails>(); }
            return results;
        }

        private class ProductDetails
        {
            public string? Product_ID { get; set; }
            public decimal Product_Price { get; set; }
            public int Product_Quantity { get; set; }
        }
    }
}
