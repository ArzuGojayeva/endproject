using EndTask.DAL;
using EndTask.Models;
using EndTask.Utilities;
using EndTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndTask.Areas.LearningAdmin.Controllers
{
    [Area("LearningAdmin")]
    [Authorize(Roles ="admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CourseController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(int page = 1, int take=2)
        {
            ViewBag.Teachers=await _context.teachers.ToListAsync();
            var course = await _context.Courses.Include(x => x.Teacher).Skip((page-1)*take).Take(take).ToListAsync();
            PaginateVM<Courses> paginateVM = new PaginateVM<Courses>()
            {
                Items = course,
                PageCount = GetPageCount(take),
                CurrentPage = page
            };
            return View(paginateVM);
        }
        public int GetPageCount(int take)
        {
            var count = _context.Courses.Count();
            return (int)Math.Ceiling((double)count / take);
        }
        public async Task< IActionResult> Create()
        {
            ViewBag.Teachers = await _context.teachers.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Courses courses)
        {
            ViewBag.Teachers = await _context.teachers.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if (courses == null) { return View(); } 
            if(courses.ImageFile!= null)
            {
                if (!courses.ImageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("", "error");
                    return View();
                }
                if (!courses.ImageFile.CheckFileSize(2000))
                {
                    ModelState.AddModelError("", "error");
                    return View();
                }
                courses.Image=await courses.ImageFile.SavefileAsync(_environment.WebRootPath,"assets/img");
            }
            await _context.Courses.AddAsync(courses);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Edit(int id)
        {
            ViewBag.Teachers = await _context.teachers.ToListAsync();
            return View(await _context.Courses.Include(x=>x.Teacher).FirstOrDefaultAsync(x=>x.Id==id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Courses courses)
        {
            ViewBag.Teachers = await _context.teachers.ToListAsync();
            Courses? exist = await _context.Courses.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id == courses.Id);
            if (!ModelState.IsValid) { return View(); }
            if (exist == null) { return View(); }
            if (!courses.ImageFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            if (!courses.ImageFile.CheckFileSize(2000))
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            string path = Path.Combine(_environment.WebRootPath, "assets/img",exist.Image);
            if (System.IO.File.Exists(path)){
                System.IO.File.Delete(path);    
            }
            exist.Image = await courses.ImageFile.SavefileAsync(_environment.WebRootPath, "assets/img");
            exist.Name=courses.Name;
            exist.Teacher = courses.Teacher;
            exist.TeacherId= courses.TeacherId;
            exist.Id= courses.Id;
            exist.Price= courses.Price;   
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");   
        }
        public async Task<IActionResult>Delete(int id)
        {
            Courses? exist = await _context.Courses.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id == id);
            if (!ModelState.IsValid) { return View(); }
            if(exist == null) { return View(); }
            string path = Path.Combine(_environment.WebRootPath, "assets/img", exist.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Courses.Remove(exist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
