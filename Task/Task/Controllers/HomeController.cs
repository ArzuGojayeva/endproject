using Microsoft.AspNetCore.Mvc;
using Task.DAL;
using Task.ViewModels;

namespace Task.Controllers
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
                blogs=_context.Blogs.ToList(),
                slider=_context.sliders.FirstOrDefault(),
                OurServices=_context.OurServices.Take(6).ToList(),
                Teams=_context.Teams.Take(4).ToList(),
            };
            return View(homeVM);
        }
    }
}
