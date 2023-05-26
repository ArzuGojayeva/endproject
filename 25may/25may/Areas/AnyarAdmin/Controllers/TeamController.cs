using _25may.DAL;
using _25may.Models;
using _25may.Utilities;
using _25may.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _25may.Areas.AnyarAdmin.Controllers
{
    [Area("AnyarAdmin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index(int take=2,int page=1)
        {
          var team=_context.team.Include(x => x.Profession).Skip((page-1)*take).Take(take).ToList();
            PaginateVM<Team> paginateVM = new PaginateVM<Team>()
            {
                Items = team,
                PageCount = GetPageCount(take),
                CurrentPage = page,
            };
            return View(paginateVM);
          
        }
        public int GetPageCount(int take)
        {
            var count=_context.team.Count();
            return (int)Math.Ceiling((double)count/ take);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Profession =await _context.Professions.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if (team == null) { return View(); }
            if (!team.ImageFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            if (!team.ImageFile.CheckFileSize(2000))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            team.Image = await team.ImageFile.SavefileAsync(_environment.WebRootPath, "assets/img/team");
            await _context.team.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            return View(_context.team.Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            Team? exist = await _context.team.Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == team.Id);
            if (!ModelState.IsValid) { return View(); }
            if (exist == null) { return View(); }
            if (!team.ImageFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            if (!team.ImageFile.CheckFileSize(2000))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            string path = Path.Combine(_environment.WebRootPath, "assets/img/team", team.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            exist.Image = await team.ImageFile.SavefileAsync(_environment.WebRootPath, "assets/img/team");
            exist.Profession = team.Profession;
            exist.ProfessionId = team.ProfessionId;
            exist.Name = team.Name;
            exist.Id = team.Id;
            exist.Description = team.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Team? exist = await _context.team.Include(x => x.Profession).FirstOrDefaultAsync(x => x.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(exist == null) { return View(); }    
            string path = Path.Combine(_environment.WebRootPath, "assets/img/team",exist.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Remove(exist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
