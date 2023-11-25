using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class TotalSalesService
    {
        public async Task<int> ProcessRequest(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return 0;
            }
            var salesTransactionsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2); 
            return ExecuteAnalysis(salesTransactionsTableFromDatabase, startDate.Value, endDate.Value);
        }

        private int ExecuteAnalysis(List<Group_1_Record_Abstraction>? salesTransactionsData, DateTime startDate, DateTime endDate)
        {
            if (salesTransactionsData == null)
            {
                return 0;
            }
            return (int)salesTransactionsData
                .OfType<SaleTransaction>()
                .Where(s => s.date >= startDate && s.date <= endDate)
                .Sum(s => s.Order_Value);
        }
    }
}
