using Microsoft.AspNetCore.Mvc;

namespace Server_Side.Controllers
{
    public class AnalyticsController : Controller
    {
        //[Route("")] // This means it maps to /analytics
        //[Route("index")] // This means it maps to /analytics/index
        public IActionResult Index()
        {
            return View();
        }

        //Route to the charts.cshtml
        [Route("analytics/charts")]
        public IActionResult Charts()
        {
            return View();
        }

        //Gets infromation that is going to be used by flex containers 
        [HttpGet("/analytic/salesData")]
        public IActionResult GetSalesData()
        {
            var data = new
            {
                salesTotal = 1000,
                viewTotal = 5000,
                lifetimeSales = 50000,
                averageSatisfaction = 4.5
            };
            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";
            // Return the data as a JSON response
            return Json(data);
        }

        //
        [HttpGet("/analytic/tableData")]
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
            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";
            // Return the data as a JSON response
            return Json(data);
        }


        //Get information that is going to be used by flex containers in Charts.cshtml
        [HttpGet("/charts/productInfoData")]
        public IActionResult GetProductInfoData()
        {
            var data = new
            {
                salesRate = 0.45,
                placeHolder = "placeholder"
            };

            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";

            // Return the data as a JSON response
            return Json(data);
        }

        [HttpGet("/charts/monthlySalesData")]
        public IActionResult GetMonthlySalesData()
        {
            var salesData = new
            {
                monthlySales = new[] {50, 75, 60, 80, 95}
            };

            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";

            return Json(salesData);
        }

        [HttpGet("/charts/monthlyViewsData")]
        public IActionResult GetMonthlyViewsData()
        {
            var viewsData = new
            {
                monthlyViews = new[] {3000, 3500, 4200, 2800, 5500}
            };

            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";
            
            return Json(viewsData);
        }

        [HttpGet("/charts/monthlySatisfactionData")]
        public IActionResult GetMonthlySatisfactionData()
        {
            var satisfactionData = new
            {
                monthlySatisfaction = new[] { 3, 3.5, 4, 4.1, 4.25, 3.85, 4.45, 4.9, 4.4, 4.3, 2.7, 4 }
            };

            // Set the Content-Type header to "application/json"
            Response.ContentType = "application/json";
            
            return Json(satisfactionData);
        }
    }
}