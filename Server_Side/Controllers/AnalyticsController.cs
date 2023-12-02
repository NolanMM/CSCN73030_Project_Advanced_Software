using Microsoft.AspNetCore.Mvc;
using Server_Side.DatabaseServices;
using Server_Side.Services;
using Server_Side.Services.Analysis_Services;
using System;
using System.Linq;

namespace Server_Side.Controllers
{
    public class ProfileViewModel
    {
        // Properties relevant to your view
        public string UserId { get; set; }
        public string ProductId { get; set; }
        // Other properties...
    }

    public class AnalyticsController : Controller
    {
        private readonly Analysis_Report_Center _reportServices;

        public AnalyticsController(Analysis_Report_Center reportServices)
        {
            _reportServices = reportServices;
        }

        //default route no profile specified
        public IActionResult Index()
        {
            return NotFound();
            //return NotFound("This Page is not found! Please Login or check browser connections. ERROR: 404");
        }

        [Route("analytics/Profile/{userId}/charts/{productId}")]
        public IActionResult Charts(string userId, string productId)
        {


            ProfileViewModel modelProfileProduct = new ProfileViewModel
            {

                UserId = userId,
                ProductId = productId,

            };

            return View(modelProfileProduct);
        }


        //index profile specified
        [Route("Profile/{userId}")]
        public IActionResult Index(string userId)
        {

            ProfileViewModel modelProfile = new ProfileViewModel
            {
                UserId = userId,
            };

            return View(modelProfile);
        }

        //[HttpGet("/analytics/salesData/Profile/{userId}")]
        //public IActionResult GetSalesData(string userId)
        //{

        //    // Replace with your actual data retrieval logic using the userId
        //    var salesTotal = 100;
        //    var viewTotal = 50;
        //    var lifetimeSales = 500;
        //    var averageSatisfaction = 4.5;

        //    var data = new
        //    {
        //        salesTotal,
        //        viewTotal,
        //        lifetimeSales,
        //        averageSatisfaction
        //    };

        //    Response.ContentType = "application/json";
        //    return Json(data);
        //}

        [HttpGet("/analytics/salesData/Profile/{userId}")]
        public async Task<IActionResult> GetSalesDataProfile(string userID)
        {
            try
            {
                var startDate = DateTime.Now.AddMonths(-1);
                var endDate = DateTime.Now;

                var salesTotalResult = await _reportServices.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null, userID);
                var viewTotalResult = await _reportServices.ProcessAnalysisReportingServicesByID(4, startDate, endDate, null, userID);
                var lifetimeSalesResult = await _reportServices.ProcessAnalysisReportingServicesByID(7, DateTime.MinValue, DateTime.MaxValue, null, userID);
                var averageSatisfactionResult = await _reportServices.ProcessAnalysisReportingServicesByID(3, DateTime.MinValue, DateTime.MaxValue, null, userID);

                int salesTotal = ConvertToInt(salesTotalResult) -1;
                int viewTotal = ConvertToInt(viewTotalResult) -1;
                int lifetimeSales = ConvertToInt(lifetimeSalesResult) - 1;
                int averageSatisfaction = ConvertToInt(averageSatisfactionResult) - 1;

                if (salesTotal < 0){ salesTotal = 0; }
                if (viewTotal < 0) {  viewTotal = 0; }
                if (lifetimeSales < 0) {  lifetimeSales = 0; }
                if (averageSatisfaction < 0) {  averageSatisfaction = 0; }

                var data = new
                {
                    salesTotal,
                    viewTotal,
                    lifetimeSales,
                    averageSatisfaction // This is now an integer
                };

                return Json(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int ConvertToInt(object? result)
        {
            if (result is null)
            {
                return 0;
            }

            if (result is int intValue)
            {
                return intValue;
            }

            if (int.TryParse(result.ToString(), out intValue))
            {
                return intValue;
            }

            return 0; // Default value if conversion fails
        }


        [HttpGet("/analytics/tableData/Profile/{userId}")]
        public async Task<IActionResult> GettableData(string userId)
        {
            userId = "S0006";
            List<ProductItemData>? returnList = (List<ProductItemData>?)await Database_Centre.ProcessDataForGetTableCorrespondingUserID(userId);

            if (returnList == null)
            {
                return NotFound(); // Or any other appropriate status code
            }

            var templateList = returnList.Select(item => new
            {
                plist = item.ProductName,
                tsales = item.TodaySale,
                tviews = item.TodayViews,
                pPrice = item.ProductPrices,
                pID = item.ProductID,
                col6 = "Details"
            }).ToList();

            Response.ContentType = "application/json";
            return Json(templateList);
        }


        [HttpGet("/charts/productInfoData/{productId}")]
        public IActionResult GetProductInfoData(string productId)
        {

            // Replace with your actual data retrieval logic using the productId
            var salesRate = 100; 
            var conversionRate = 50;

            var data = new
            {
                salesRate, 
                conversionRate,
            };

            Response.ContentType = "application/json";
            return Json(data);
        }

        [HttpGet("/charts/monthlySalesData/{productId}")]
        //public async Task<IActionResult> GetMonthlySalesData()
        public IActionResult GetMonthlySalesData(string productId)
        {

            // Replace with your actual data retrieval logic using the productId


            //numbers per month basis
            //"January",
            //"February",
            //"March",
            //"April",
            //"May",
            //"June",
            //"July",
            //"August",
            //"September",
            //"October",
            //"November",
            //"December",
            var monthlySales = new[] { 1000, 2500, 3200, 2800, 1500, 7, 100, 600, 9000, 47474, 1, 200 };


            //var startDate = new DateTime(DateTime.Now.Year, 1, 1); // Start of the current year
            //var endDate = DateTime.Now; // Current date

            //var timeAnalysisService = new TimeAnalysisService();
            //var monthlySalesResult = await timeAnalysisService.ProcessRequest(startDate, endDate);

            var salesData = new
            {
                monthlySales
                //monthlySales = monthlySalesResult.Values.ToArray() // Using the values from the TimeAnalysisService
            };

            Response.ContentType = "application/json";
            return Json(salesData);
        }

        [HttpGet("/charts/monthlyViewsData/{productId}")]
        public IActionResult GetMonthlyViewsData(string productId)
        {


            // Replace with your actual data retrieval logic using the productId


            //numbers per month basis
            //"January",
            //"February",
            //"March",
            //"April",
            //"May",
            //"June",
            //"July",
            //"August",
            //"September",
            //"October",
            //"November",
            //"December",
            var monthlyViews = new[] { 3000, 3500, 4200, 2800, 5500, 10, 1, 100, 7000, 400, 3790, 6969 };

            var viewsData = new
            {
                monthlyViews
            };

            Response.ContentType = "application/json";
            return Json(viewsData);
        }

        [HttpGet("/charts/monthlySatisfactionData/{productId}")]
        public IActionResult GetMonthlySatisfactionData(string productId)
        {


          // Replace with your actual data retrieval logic using the productId

          //numbers per month basis
          //"January",
          //"February",
          //"March",
          //"April",
          //"May",
          //"June",
          //"July",
          //"August",
          //"September",
          //"October",
          //"November",
          //"December",
            var monthlySatisfaction = new[] { 3, 3.5, 4, 4.1, 4.25, 3.85, 4.45, 4.9, 4.4, 4.3, 2.7, 4 };

            var satisfactionData = new
            {
                monthlySatisfaction
            };

            Response.ContentType = "application/json";
            return Json(satisfactionData);
        }
    }
}
