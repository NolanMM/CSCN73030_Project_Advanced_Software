using Microsoft.AspNetCore.Mvc;

namespace Server_Side.Controllers
{
    public class ChartsController : Controller
    {
        //[Route("")] // This means it maps to /analytics
        //[Route("index")] // This means it maps to /analytics/index
        public IActionResult Index()
        {
            return View();
        }
    }
}