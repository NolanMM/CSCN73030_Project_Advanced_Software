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
        private DateTime startDate;
        private DateTime endDate;

        public FeedbackAnalysisService(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                throw new ArgumentNullException("Start date and end date cannot be null");
            }
            this.startDate = startDate.Value;
            this.endDate = endDate.Value;
        }

        public async Task<Dictionary<string, int>?> ProcessRequest()
        {
            var feedbackTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(3);
            var validDataReturn = ProcessFeedbackDataAsync(feedbackTableFromDatabase);
            if (validDataReturn)
            {
                var result = AnalyzeFeedback();
                return result;
            }
            else
            {
                return null;
            }
        }

        private bool ProcessFeedbackDataAsync(List<Group_1_Record_Abstraction>? feedbackData)
        {
            return true;
        }

        private Dictionary<string, int> AnalyzeFeedback()
        {
            return new Dictionary<string, int>();
        }
    }
}
