using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class TimeAnalysisService
    {
        public TimeAnalysisService()
        {
            // init empty
        }

        public async Task<Dictionary<string, int>?> ProcessRequest(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }

            var salesTransactionsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(2);
            return ExecuteAnalysis(salesTransactionsTableFromDatabase, startDate.Value, endDate.Value);
        }

        private Dictionary<string, int>? ExecuteAnalysis(List<Group_1_Record_Abstraction>? salesTransactionsData, DateTime startDate, DateTime endDate)
        {
            if (salesTransactionsData == null)
            {
                return null;
            }

            return salesTransactionsData
                .OfType<SaleTransaction>()
                .Where(s => s.date >= startDate && s.date <= endDate)
                .GroupBy(s => s.date.Month)
                .ToDictionary(grp => $"Month {grp.Key}", grp => grp.Count());
        }
    }
}
