using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Task.DAL;
using Task.Models;
using Task.Utilities;
using Task.ViewModels;

namespace Task.Areas.BusinessAdmin.Controllers
{
    [Area("BusinessAdmin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envireonment;
        public BlogController(AppDbContext context, IWebHostEnvironment envireonment)
        {
            _context = context;
            _envireonment = envireonment;
        }

        public IActionResult Index(int take=2,int page=1)
        {
            var blogcount=_context.Blogs.Skip((page-1)*take).Take(take).ToList();
            PaginateVM<Blog> paginateVM = new PaginateVM<Blog>()
            {
                Itens= blogcount,
                PageCount= GetPageCount(take),
                CurrentPage=page
            };
            return View(paginateVM);
        }
        public int GetPageCount(int take)
        {
            var count = _context.Blogs.Count();
            return (int)Math.Ceiling((double)count / take);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) { return View(); }
            if(blog != null) {
                if (!blog.ImageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
                if (!blog.ImageFile.CheckFileSize(2000))
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
                blog.Image = await blog.ImageFile.SaveFileasync(_envireonment.WebRootPath, "assets/img/blog");
            }
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }
        public async Task<IActionResult>Edit(int id)
        {
            return View(await _context.Blogs.FirstOrDefaultAsync(x=>x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult>Edit(Blog blog)
        {
            Blog? exist=await _context.Blogs.FirstOrDefaultAsync(x=>x.Id==blog.Id);
            if (!ModelState.IsValid) { return View(); }

            if (exist == null) { return View(); }

            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
                if (!blog.ImageFile.CheckFileSize(2000))
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
                string path = Path.Combine(_envireonment.WebRootPath, "assets/img/blog",exist.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exist.Image = await blog.ImageFile.SaveFileasync(_envireonment.WebRootPath, "assets/img/blog"); 
            }
           
            exist.Title=blog.Title;
            exist.Description=blog.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(int id)
        {
            Blog?exist=await _context.Blogs.FirstOrDefaultAsync(x=>x.Id==id);
            if (!ModelState.IsValid) { return View(); }
            string path = Path.Combine(_envireonment.WebRootPath, "assets/img/blog",exist.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Blogs.Remove(exist);   
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
