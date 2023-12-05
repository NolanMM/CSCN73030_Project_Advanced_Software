using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class FeedbackAnalysisService
    {
        public static async Task<int[]>? Process(DateTime? startDate, DateTime? endDate, string Product_ID)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }
            var FeedBackTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(3);
            return ExecuteAnalysis(FeedBackTableFromDatabase, startDate.Value, endDate.Value, Product_ID);
        }

        private static int[] ExecuteAnalysis(List<Group_1_Record_Abstraction>? feedbackTableFromDatabase, DateTime startDate, DateTime endDate, string productID)
        {
            if (feedbackTableFromDatabase == null || productID == null)
            {
                return new int[12];
            }

            var filteredFeedback = feedbackTableFromDatabase
                .Where(feedback => feedback is Feedback f && f.Product_ID == productID && f.Date_Updated >= startDate && f.Date_Updated <= endDate)
                .ToList();

            // Initialize array to store the sum of stars for each month
            double[] monthlySum = new double[12];
            // Initialize array to store the count of feedback records for each month
            int[] monthlyCount = new int[12];

            foreach (Feedback feedback in filteredFeedback)
            {
                int monthIndex = feedback.Date_Updated.Month - 1;
                monthlySum[monthIndex] += (double)feedback.Stars_Rating;
                monthlyCount[monthIndex]++;
            }

            int[] monthlyAverage = new int[12];
            for (int i = 0; i < 12; i++)
            {
                monthlyAverage[i] = monthlyCount[i] == 0 ? 0 : (int)(monthlySum[i] / monthlyCount[i]);
            }

            return monthlyAverage;
        }
    }
}
