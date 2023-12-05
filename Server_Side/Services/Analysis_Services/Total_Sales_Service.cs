using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.Globalization;

namespace Server_Side.Services.Analysis_Services
{
    public class TotalSalesService
    {
        public async Task<int> ProcessRequest(DateTime? startDate, DateTime? endDate, string? UserID)
        {
            if (startDate == null || endDate == null)
            {
                return 0;
            }
            var salesTransactionsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            return ExecuteAnalysis(salesTransactionsTableFromDatabase, startDate.Value, endDate.Value, UserID);
        }

        private int ExecuteAnalysis(List<Group_1_Record_Abstraction>? salesTransactionsData, DateTime startDate, DateTime endDate, string? userID)
        {
            if (userID == null)
            {
                return 0;
            }
            List<SaleTransaction> saleTransactionsList = new List<SaleTransaction>();

            // Filter SaleTransactions from the given salesTransactionsData
            foreach (var myObject in salesTransactionsData ?? Enumerable.Empty<Group_1_Record_Abstraction>())
            {
                if (myObject is SaleTransaction saleTransaction)
                {
                    saleTransactionsList.Add(saleTransaction);
                }
            }

            SaleTransactionTableService saleTransactionTableService = new SaleTransactionTableService();

            // Process SaleTransactionList and get the result dictionary
            Dictionary<(string, string), string>? returnResultListProductLinkToThatUserId =
                saleTransactionTableService.ProcessSaleTransactionList(saleTransactionsList, userID);

            // Check if the date inside the key of the dictionary is valid
            int totalQuantity = 0;
            if (returnResultListProductLinkToThatUserId != null)
            {
                foreach (var kvp in returnResultListProductLinkToThatUserId)
                {
                    if (DateTime.TryParseExact(kvp.Key.Item2, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime transactionDate)
                        && startDate <= transactionDate && transactionDate <= endDate)
                    {
                        // Valid date, add quantity to total
                        if (int.TryParse(kvp.Value, out int quantity))
                        {
                            totalQuantity += quantity;
                        }
                    }
                }
            }

            return totalQuantity;
        }
    }
}