using Newtonsoft.Json;
using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.Services.Analysis_Services
{
    public class Monthly_View_Service
    {
        public static async Task<int[]>? Process(DateTime? startDate, DateTime? endDate, string Product_ID)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }
            var UserViewTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(0);
            return ExecuteAnalysis(UserViewTableFromDatabase, startDate.Value, endDate.Value, Product_ID);
        }
        private static int[] ExecuteAnalysis(List<Group_1_Record_Abstraction>? UserViewTableFromDatabase, DateTime startDate, DateTime endDate, string ProductID)
        {
            if (UserViewTableFromDatabase == null || ProductID == null)
            {
                return new int[12];
            }

            var monthlyViews = Enumerable.Range(1, 12)
                .Select(month =>
                {
                    var viewsForMonth = UserViewTableFromDatabase
                        .Count(userView =>
                            userView is UserView uv &&
                            uv.Product_ID == ProductID &&
                            uv.Date_Access.Date >= new DateTime(startDate.Year, month, 1) &&
                            uv.Date_Access.Date <= new DateTime(endDate.Year, month, DateTime.DaysInMonth(endDate.Year, month))
                        );

                    return viewsForMonth;
                })
                .ToArray();

            return monthlyViews;
        }
    }
}
