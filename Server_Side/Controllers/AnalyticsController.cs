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

        public IActionResult Index()
        {
            return View();
        }

        [Route("analytics/charts")]
        public IActionResult Charts()
        {
            return View();
        }

        [HttpGet("/analytics/salesData")]
        [HttpGet("/analytics/salesData")]
        public async Task<IActionResult> GetSalesData()
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            var salesTotalResult = await _reportServices.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null); // Assuming 7 is TotalSalesService
            var viewTotalResult = await _reportServices.ProcessAnalysisReportingServicesByID(4, startDate, endDate, null); // Assuming 4 is PageViewsService
            var lifetimeSalesResult = await _reportServices.ProcessAnalysisReportingServicesByID(7, DateTime.MinValue, DateTime.MaxValue, null); // Assuming 7 is TotalSalesService for lifetime sales
            var averageSatisfactionResult = await _reportServices.ProcessAnalysisReportingServicesByID(3, DateTime.MinValue, DateTime.MaxValue, null); // Assuming 3 is FeedbackAnalysisService

            // Convert results to int (assuming they are already int or can be safely cast to int)
            var salesTotal = ConvertToInt(salesTotalResult);
            var viewTotal = ConvertToInt(viewTotalResult);
            var lifetimeSales = ConvertToInt(lifetimeSalesResult);
            var averageSatisfaction = ConvertToInt(averageSatisfactionResult);

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

        private int ConvertToInt(object result)
        {
            // Safely convert the result to int, returning 0 if the conversion isn't possible
            return (result != null && int.TryParse(result.ToString(), out int intValue)) ? intValue : 0;
        }


        [HttpGet("/analytics/tableData")]
        public IActionResult GettableData()
        {
            var data = new[]
            {
                new
                {
                    col1 = "Row 1, Col 1",
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
