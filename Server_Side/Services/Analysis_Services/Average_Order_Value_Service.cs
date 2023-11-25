using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.Services.Analysis_Services
{
    public class AverageOrderValueService
    {
        public static List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        private DateTime startDate;
        private DateTime endDate;
        public AverageOrderValueService(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) 
            { 
                return; 
            }
            else { 
                this.startDate = startDate.Value; this.endDate = endDate.Value; 
            }
        }
        public async Task<decimal?> ProcessRequest()
        {
            SalesTransactionsTable.Clear();
            var saleTransactionTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            var validDataReturn = ProcessListSaleTransactionTable_DataAsync(saleTransactionTableFromDatabase);
            if(validDataReturn)
            {
                var result_Process = ExecuteAnalysis();
                return result_Process;
            }
            else
            {
                return null;
            }
        }
        public static bool ProcessListSaleTransactionTable_DataAsync(List<Group_1_Record_Abstraction>? dataAsList)
        {
            try
            {
                SalesTransactionsTable.Clear();
                foreach (var Myobject in dataAsList)
                {
                    if (Myobject is SaleTransaction saleTransaction)
                    {
                        SalesTransactionsTable.Add(saleTransaction);
                    }
                    else
                    {
                        Console.WriteLine("Unknown object type");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string dataContent = "Error: " + ex.Message;
                return false;
            }
        }
        private int ExecuteAnalysis()
        {
            if (SalesTransactionsTable == null)
                throw new InvalidOperationException("SalesTransactions data is not initialized.");

            var relevantSales = SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .ToList();

            return relevantSales.Any() ? (int)Math.Round(relevantSales.Average(s => s.Order_Value)) : 0;
        }


    }
}
