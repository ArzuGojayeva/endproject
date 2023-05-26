using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task.Areas.BusinessAdmin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("BusinessAdmin")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
