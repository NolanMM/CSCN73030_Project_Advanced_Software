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
    }
}