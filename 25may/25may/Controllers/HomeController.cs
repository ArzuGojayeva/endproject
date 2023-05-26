using _25may.DAL;
using _25may.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _25may.Controllers
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
            HomeVM homeVM = new HomeVM()
            {
                Teams=_context.team.Include(x=>x.Profession).Take(6).ToList(),
                OurServices=_context.OurServices.Take(6).ToList(),
                Pricings=_context.Pricing.Take(4).ToList()
            };
            return View(homeVM);
        }
    }
}
