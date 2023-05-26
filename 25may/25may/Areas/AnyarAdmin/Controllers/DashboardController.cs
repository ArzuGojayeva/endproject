using Microsoft.AspNetCore.Mvc;

namespace _25may.Areas.AnyarAdmin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("AnyarAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
