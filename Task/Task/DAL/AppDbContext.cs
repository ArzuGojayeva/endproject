using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Blog>Blogs { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Setting> Setting { get; set; }
    }
}
