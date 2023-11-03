using Microsoft.AspNetCore.Mvc;
using Server_Side.Services;
using System;
using System.Linq;

namespace Server_Side.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly Analysis_Report_Services _reportServices;

        public AnalyticsController(Analysis_Report_Services reportServices)
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
        public IActionResult GetSalesData()
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            var data = new
            {
                salesTotal = _reportServices.GetTotalSales(startDate, endDate),
                viewTotal = _reportServices.GetPageViews(startDate, endDate).Sum(x => x.Value),
                lifetimeSales = _reportServices.GetSalesAnalysis().Sum(x => x.Value),
                averageSatisfaction = _reportServices.GetFeedbackAnalysis().Average(x => int.Parse(x.Key) * x.Value) // Simplified
            };

            Response.ContentType = "application/json";
            return Json(data);
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
        public IActionResult GetProductInfoData()
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            var data = new
            {
                salesRate = _reportServices.GetConversionRate("", startDate, endDate), // Placeholder for product ID
                placeHolder = "placeholder"
            };

            Response.ContentType = "application/json";
            return Json(data);
        }

        [HttpGet("/charts/monthlySalesData")]
        public IActionResult GetMonthlySalesData()
        {
            var monthlySales = _reportServices.GetTimeAnalysis().Values.ToArray();

            var salesData = new
            {
                monthlySales
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
