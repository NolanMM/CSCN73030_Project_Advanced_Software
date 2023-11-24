using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class UniqueVisitorsService
    {
        private readonly Analysis_Report_Center _reportCenter;

        public UniqueVisitorsService(Analysis_Report_Center reportCenter)
        {
            _reportCenter = reportCenter ?? throw new ArgumentNullException(nameof(reportCenter));
        }

        public async Task<int> GetUniqueVisitorCountAsync(DateTime startDate, DateTime endDate)
        {
            // Validate the date range
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be less than or equal to end date.");
            }

            return await Task.Run(() =>
            {
                // Calculate the count of unique visitors
                return _reportCenter.Valid_User_Views_Table
                    .Where(v => v.Date_Access >= startDate && v.Date_Access <= endDate)
                    .Select(v => v.User_ID)
                    .Distinct()
                    .Count();
            });
        }
    }
}
