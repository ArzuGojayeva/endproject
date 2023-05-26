using EndTask.DAL;
using EndTask.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM=new HomeVM()
            {
                Courses=_context.Courses.Take(3).OrderByDescending(x=>x.Id).ToList(),
                Teachers=_context.teachers.Take(4).ToList(),
                Services=_context.OurServices.Take(4).ToList(),
            };
            return View(homeVM);
        }
    }
}
