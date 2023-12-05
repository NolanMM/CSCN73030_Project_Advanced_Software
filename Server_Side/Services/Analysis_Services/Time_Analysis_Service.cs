using Newtonsoft.Json;
using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.Services.Analysis_Services
{
    public class TimeAnalysisService
    {
        public async static Task<int[]?> ProcessRequest(DateTime? startDate, DateTime? endDate, string ?ProductID)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }

            var salesTransactionsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            return ExecuteAnalysis(salesTransactionsTableFromDatabase, startDate, endDate, ProductID);
        }

        private static int[] ExecuteAnalysis(List<Group_1_Record_Abstraction>? salesTransactionsData, DateTime? startDate, DateTime? endDate, string? ProductID)
        {
            if (salesTransactionsData == null)
            {
                return new int[12]; // Return an array with 12 elements initialized to 0
            }

            var monthlySales = Enumerable.Range(1, 12)
                .Select(month =>
                {
                    var totalQuantity = salesTransactionsData
                        .OfType<SaleTransaction>()
                        .Where(s => s.date.Month == month && s.date >= startDate && s.date <= endDate)
                        .SelectMany(s => ParseProductDetails(s.Details_Products, month))
                        .Where(p => p.Product_ID == ProductID)
                        .Sum(p => p.Product_Quantity);

                    return totalQuantity;
                })
                .ToArray();

            return monthlySales;
        }

        private static List<ProductDetails>  ParseProductDetails(string detailsProducts, int Month)
        {
            // Replace single quotes with double quotes
            detailsProducts = detailsProducts.Replace("'", "\"");

            // Deserialize the modified JSON string
            var results = JsonConvert.DeserializeObject<List<ProductDetails>>(detailsProducts);
            if (results != null)
            {
                foreach (ProductDetails productDetails in results)
                {
                    productDetails.month = Month;
                }
            }
            else
            {
                results ??= new List<ProductDetails>();
            }
            return results;
        }

        private class ProductDetails
        {
            public string Product_ID { get; set; } = string.Empty;
            public decimal Product_Price { get; set; }
            public int Product_Quantity { get; set; }
            public int month {  get; set; }
        }
    }
}
