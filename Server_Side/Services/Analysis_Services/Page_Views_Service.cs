//using Server_Side.DatabaseServices;
//using Server_Side.DatabaseServices.Services.Model;
//using Server_Side.DatabaseServices.Services.Models.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Server_Side.Services.Analysis_Services
//{
//    public class PageViewsService
//    {
//        public PageViewsService()
//        {
//            // Additional initialization, if needed
//        }

//        public async Task<Dictionary<string, int>?> ProcessRequest(DateTime? startDate, DateTime? endDate)
//        {
//            if (startDate == null || endDate == null)
//            {
//                return null;
//            }

//            var pageViewsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(1);
//            var relevantPageViews = ProcessPageViewsData(pageViewsTableFromDatabase, startDate.Value, endDate.Value);
//            return relevantPageViews;
//        }

//        private Dictionary<string, int>? ProcessPageViewsData(List<Group_1_Record_Abstraction>? pageViewsData, DateTime startDate, DateTime endDate)
//        {
//            if (pageViewsData == null)
//            {
//                return null;
//            }

//            var pageViewsByPageName = pageViewsData
//                .OfType<PageView>()
//                .Where(pv => pv.Start_Time >= startDate && pv.Start_Time <= endDate)
//                .GroupBy(pv => pv.Page_Name)
//                .ToDictionary(group => group.Key, group => group.Count());

//            return pageViewsByPageName;
//        }
//    }
//}

using Server_Side.DatabaseServices;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Side.Services.Analysis_Services
{
    public class PageViewsService
    {
        public async Task<int?> ProcessRequest(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }

            var pageViewsTableFromDatabase = await Database_Centre.GetDataForDatabaseServiceID(1);
            return ProcessPageViewsData(pageViewsTableFromDatabase, startDate.Value, endDate.Value);
        }

        private int? ProcessPageViewsData(List<Group_1_Record_Abstraction>? pageViewsData, DateTime startDate, DateTime endDate)
        {
            if (pageViewsData == null)
            {
                return null;
            }

            int count = pageViewsData
                .OfType<PageView>()
                .Count(pv => pv.Start_Time >= startDate && pv.Start_Time <= endDate);

            return count;
        }
    }
}
