using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.Services.Analysis_Services
{
    public class UniqueVisitorsService
    {
        public async Task<int> GetUniqueVisitorCountAsync(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                throw new ArgumentNullException("Start date and end date cannot be null.");
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be less than or equal to end date.");
            }

            var userViewsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(0);
            return CalculateUniqueVisitors(userViewsTableFromDatabase, startDate.Value, endDate.Value);
        }

        private int CalculateUniqueVisitors(List<Group_1_Record_Abstraction>? userViewsData, DateTime startDate, DateTime endDate)
        {
            if (userViewsData == null)
            {
                return 0;
            }

            return userViewsData
                .OfType<UserView>()
                .Where(v => v.Date_Access >= startDate && v.Date_Access <= endDate)
                .Select(v => v.User_ID)
                .Distinct()
                .Count();
        }
    }
}
