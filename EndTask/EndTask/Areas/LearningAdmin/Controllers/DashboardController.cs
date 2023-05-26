using Microsoft.AspNetCore.Mvc;

namespace EndTask.Areas.LearningAdmin.Controllers
{
    [Area("LearningAdmin")]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
