using Microsoft.AspNetCore.Mvc;
using Server_Side.Services;
using Server_Side.Services.Analysis_Services;
using System;
using System.Linq;

namespace Server_Side.Controllers
{
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
            return View();
        }

        [Route("analytics/charts")]
        public IActionResult Charts()
        {
            return View();
        }

        //index profile specified
        [Route("Profile/{userId}")]
        public IActionResult Index(string userId)
        {
            //return Content("Proile for " + userId);
            ViewBag.UserId = userId;
            return View();
        }

        [HttpGet("/analytics/salesData/Profile/{userId}")]
        public async Task<IActionResult> GetSalesData(string userId)
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            

            //var salesTotal = await _reportServices.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null); //number needs to be changed
            var salesTotal = 10;
            var viewTotal = await _reportServices.ProcessAnalysisReportingServicesByID(4, startDate, endDate, null); //number needs to be changed
            var lifetimeSales = await _reportServices.ProcessAnalysisReportingServicesByID(7, DateTime.MinValue, DateTime.MaxValue, null); //number needs to be changed
            var averageSatisfaction = await _reportServices.ProcessAnalysisReportingServicesByID(3, DateTime.MinValue, DateTime.MaxValue, null); //number needs to be changed

            var data = new
            {
                salesTotal,
                viewTotal,
                lifetimeSales,
                averageSatisfaction
            };

            Response.ContentType = "application/json";
            return Json(data);
        }

        [HttpGet("/analytics/tableData/Profile/{userId}")]
        public IActionResult GettableData(string userId)
        {
            var data = new[]
            {
                new
                {
                    col1 = "Row 10, Col 1",
                    col2 = "Row 1, Col 2",
                    col3 = "Row 1, Col 3",
                    col4 = "Row 1, Col 4",
                    col5 = "Details"
                },
                new
                {
                    col1 = "Row 2, Col 1",
                    col2 = "Row 2, Col 2",
                    col3 = "Row 2, Col 3",
                    col4 = "Row 2, Col 4",
                    col5 = "Details"
                },
                new
                {
                    col1 = "Row 3, Col 1",
                    col2 = "Row 3, Col 2",
                    col3 = "Row 3, Col 3",
                    col4 = "Row 3, Col 4",
                    col5 = "Details"
                }
            };
            Response.ContentType = "application/json";
            return Json(data);
        }

        [HttpGet("/charts/productInfoData")]
        public async Task<IActionResult> GetProductInfoData(string productId)
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            var conversionRateService = new ConversionRateService(startDate, endDate, productId);
            var salesRateResult = await conversionRateService.ProcessRequest();

            var data = new
            {
                salesRate = salesRateResult, // Using the result from the ConversionRateService
                placeHolder = "placeholder"
            };

            Response.ContentType = "application/json";
            return Json(data);
        }

        [HttpGet("/charts/monthlySalesData")]
        public async Task<IActionResult> GetMonthlySalesData()
        {
            var startDate = new DateTime(DateTime.Now.Year, 1, 1); // Start of the current year
            var endDate = DateTime.Now; // Current date

            var timeAnalysisService = new TimeAnalysisService();
            var monthlySalesResult = await timeAnalysisService.ProcessRequest(startDate, endDate);

            var salesData = new
            {
                monthlySales = monthlySalesResult.Values.ToArray() // Using the values from the TimeAnalysisService
            };

            Response.ContentType = "application/json";
            return Json(salesData);
        }

        [HttpGet("/charts/monthlyViewsData")]
        public IActionResult GetMonthlyViewsData()
        {
            var monthlyViews = new[] { 3000, 3500, 4200, 2800, 5500 };

            var viewsData = new
            {
                monthlyViews
            };

            Response.ContentType = "application/json";
            return Json(viewsData);
        }

        [HttpGet("/charts/monthlySatisfactionData")]
        public IActionResult GetMonthlySatisfactionData()
        {
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
