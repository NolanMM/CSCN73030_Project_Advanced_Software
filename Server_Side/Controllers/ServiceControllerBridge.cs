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
        public string? plist;
        public string? tsales;
        public string? tviews;
        public string? pPrice;
        public string? pID;
        public string? col6;
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
                    tempData.salesTotal = (int)await _reportServices.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null); //number needs to be changed
                    tempData.viewTotal = (int)await _reportServices.ProcessAnalysisReportingServicesByID(4, startDate, endDate, null); //number needs to be changed
                    tempData.lifetimeSales = (int)await _reportServices.ProcessAnalysisReportingServicesByID(7, DateTime.MinValue, DateTime.MaxValue, null); //number needs to be changed
                    tempData.averageSatisfaction = (decimal)await _reportServices.ProcessAnalysisReportingServicesByID(3, DateTime.MinValue, DateTime.MaxValue, null); //number needs to be changed
                    return tempData as object;
                }
                if (request == "GettableData")
                {

                    List<ProductItemData>? ProcessDataForGetTableCorrespondingUserID = new List<ProductItemData>();
                    ProcessDataForGetTableCorrespondingUserID = await Database_Centre.ProcessDataForGetTableCorrespondingUserID(userID);




                    List <GettableData_Struct> data = new List<GettableData_Struct>
                    {
                        new GettableData_Struct
                        {
                            plist = "Honey",
                            tsales = "10",
                            tviews = "50",
                            pPrice = "$20",
                            pID = "honey123",
                            col6 = "Details"
                        },
                        new GettableData_Struct
                        {
                            plist = "Row 2, Col 1",
                            tsales = "Row 2, Col 2",
                            tviews = "Row 2, Col 3",
                            pPrice = "Row 2, Col 4",
                            pID = "shoes543",
                            col6 = "Details"
                        },
                        new GettableData_Struct
                        {
                            plist = "Row 3, Col 1",
                            tsales = "Row 3, Col 2",
                            tviews = "Row 3, Col 3",
                            pPrice = "Row 3, Col 4",
                            pID = "pants8383929",
                            col6 = "Details"
                        }
                    };

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
