using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services.Analysis_Services
{
    public class BestCategoryAnalysisService
    {
        public static List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        private DateTime startDate;
        private DateTime endDate;
        public BestCategoryAnalysisService(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return;
            }
            else
            {
                this.startDate = startDate.Value; this.endDate = endDate.Value;
            }
        }
        public async Task<Dictionary<string, int>?> ProcessRequest()
        {
            SalesTransactionsTable.Clear();
            var saleTransactionTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            var validDataReturn = ProcessListSaleTransactionTable_DataAsync(saleTransactionTableFromDatabase);
            if (validDataReturn)
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
                if (dataAsList == null) { return false; }
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

        private Dictionary<string, int> ExecuteAnalysis()
        {
            if (SalesTransactionsTable == null)
                throw new InvalidOperationException("SalesTransactions data is not initialized.");

            return SalesTransactionsTable
                .Where(s => s.date >= startDate && s.date <= endDate)
                .GroupBy(s => s.User_ID) // Grouping by User_ID 
                .Select(group => new {
                    UserID = group.Key,
                    TotalSales = (int)group.Sum(s => s.Order_Value) // Converting TotalSales to int
                })
                .OrderByDescending(result => result.TotalSales)
                .ToDictionary(result => result.UserID, result => result.TotalSales);
        }

    }
}
