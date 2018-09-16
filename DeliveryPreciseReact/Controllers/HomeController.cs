using Microsoft.AspNetCore.Mvc;

namespace DeliveryPreciseReact
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}