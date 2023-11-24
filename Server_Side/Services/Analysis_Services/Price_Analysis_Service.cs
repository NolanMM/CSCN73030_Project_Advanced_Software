using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class PriceAnalysisService
    {
        public PriceAnalysisService()
        {
            //init empty
        }

        public async Task<decimal?> ProcessRequest(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }

            var salesTransactionsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(5);
            return ExecuteAnalysis(salesTransactionsTableFromDatabase, startDate.Value, endDate.Value);
        }

        private decimal? ExecuteAnalysis(List<Group_1_Record_Abstraction>? salesTransactionsData, DateTime startDate, DateTime endDate)
        {
            if (salesTransactionsData == null)
            {
                return null;
            }

            var relevantSales = salesTransactionsData
                .OfType<SaleTransaction>()
                .Where(s => s.date >= startDate && s.date <= endDate)
                .ToList();

            decimal averageOrderValue = relevantSales.Any() ? relevantSales.Average(s => s.Order_Value) : 0;

            return averageOrderValue;
        }
    }
}
