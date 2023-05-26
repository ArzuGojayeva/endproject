using EndTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndTask.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) {

        }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Galery> Galery { get; set; }
        public DbSet<Contact> Contacts { get; set; }

      
    }
}
