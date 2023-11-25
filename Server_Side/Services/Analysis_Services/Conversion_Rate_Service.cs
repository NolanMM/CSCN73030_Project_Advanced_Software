using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server_Side.Services.Analysis_Services
{
    public class ConversionRateService
    {
        private DateTime startDate;
        private DateTime endDate;
        private string productId;
        private static List<PageView> Website_logs_table = new List<PageView>();
        private static List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        public ConversionRateService(DateTime startDate, DateTime endDate, string productId)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.productId = productId;
        }

        public async Task<Dictionary<string, decimal>?> ProcessRequest()
        {
            SalesTransactionsTable.Clear();
            var saleTransactionTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            var WeblogTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(1); 
            var valid_Weblog = ProcessListWeblogTable_DataAsync(WeblogTableFromDatabase);
            var validDataReturn = ProcessListSaleTransactionTable_DataAsync(saleTransactionTableFromDatabase);
            if (validDataReturn && valid_Weblog)
            {
                var result_Process = ExecuteAnalysis();
                return result_Process;
            }
            else
            {
                return null;
            }
        }

        public static bool ProcessListWeblogTable_DataAsync(List<Group_1_Record_Abstraction>? dataAsList)
        {
            try
            {
                Website_logs_table.Clear();
                foreach (var Myobject in dataAsList)
                {
                    if (Myobject is PageView saleTransaction)
                    {
                        Website_logs_table.Add(saleTransaction);
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


        public Dictionary<string, decimal> ExecuteAnalysis()
        {
            if (SalesTransactionsTable == null || Website_logs_table == null)
                throw new InvalidOperationException("SalesTransactionsTable or Website_logs_table data is not initialized.");//catch exception

            var productPageViews = Website_logs_table
                .Where(p => p.Product_ID == productId && p.Start_Time >= startDate && p.Start_Time <= endDate)
                .Count();

            var productSales = SalesTransactionsTable
                .Where(s => s.Product_ID == productId && s.date >= startDate && s.date <= endDate)
                .Count();

            if (productPageViews == 0)
                return new Dictionary<string, decimal> { { productId, 0 } };

            decimal conversionRate = (decimal)productSales / productPageViews;

            return new Dictionary<string, decimal>
            {
                { productId, conversionRate }
            };
        }
    }
}
