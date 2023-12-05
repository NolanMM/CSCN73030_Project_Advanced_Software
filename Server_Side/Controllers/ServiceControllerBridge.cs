using Microsoft.AspNetCore.Mvc;
using Server_Side.DatabaseServices;

namespace Server_Side.Controllers
{
    public class GetSalesDataProfile_Data
    {
        public int? salesTotal;
        public int? viewTotal;
        public int? lifetimeSales;
        public decimal? averageSatisfaction;
    }

    public class GettableData_Struct
    {
        public string? plist;       // ProductName
        public string? tsales;      // TodaySale
        public string? tviews;      // TodayViews
        public string? pPrice;      // ProductPrices
        public string? pID;         // ProductID
        public string? col6;        // 
    }
    public abstract class ServiceControllerBridege
    {
        private static readonly Analysis_Report_Center _reportServices = new Analysis_Report_Center();
        public async static Task<object?> ProcessTheRequest(string request, string? userID, DateTime? startDate, DateTime? endDate)
        {
            try
            {

                if (request == null)
                {
                    return null;
                }
                if (request == "GetSalesDataProfile")
                {
                    GetSalesDataProfile_Data tempData = new GetSalesDataProfile_Data();
                    tempData.salesTotal = (int)await _reportServices.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null, userID); //number needs to be changed
                    tempData.viewTotal = (int)await _reportServices.ProcessAnalysisReportingServicesByID(4, startDate, endDate, null, userID); //number needs to be changed
                    tempData.lifetimeSales = (int)await _reportServices.ProcessAnalysisReportingServicesByID(7, DateTime.MinValue, DateTime.MaxValue, null, userID); //number needs to be changed
                    tempData.averageSatisfaction = (decimal)await _reportServices.ProcessAnalysisReportingServicesByID(3, DateTime.MinValue, DateTime.MaxValue, null, userID); //number needs to be changed
                    return tempData as object;
                }
                if (request == "GettableData")
                {
                    List<ProductItemData>? ProcessDataForGetTableCorrespondingUserID = new List<ProductItemData>();
                    ProcessDataForGetTableCorrespondingUserID = await Database_Centre.ProcessDataForGetTableCorrespondingUserID(userID,startDate.Value,endDate.Value);

                    List<GettableData_Struct> data = new List<GettableData_Struct>();

                    foreach(ProductItemData productItemData in ProcessDataForGetTableCorrespondingUserID)
                    {
                        GettableData_Struct data_input = new GettableData_Struct
                        {
                            plist = productItemData.ProductName,
                            tsales = productItemData.TodaySale,
                            tviews = productItemData.TodaySale,
                            pPrice = productItemData.ProductPrices,
                            pID = productItemData.ProductID,
                            col6 = "Details"
                        };
                        data.Add(data_input);
                    }
 
                    return data as object;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
   }
}
