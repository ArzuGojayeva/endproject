using _25may.DAL;
using _25may.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _25may.Areas.AnyarAdmin.Controllers
{
    [Area("AnyarAdmin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        public ServicesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Settings.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult>Edit(Settings settings)
        {
            Settings?exist= await _context.Settings.FirstOrDefaultAsync(x=>x.Id== settings.Id);
            if(!ModelState.IsValid) { return View();}
            if(exist==null) { return View(); }
            exist.Id=settings.Id;
            exist.Value = settings.Value;
            exist.Key= settings.Key;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
