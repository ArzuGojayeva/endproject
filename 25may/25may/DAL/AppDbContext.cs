using _25may.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _25may.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Team>team { get; set; }
        public DbSet<Profession>Professions { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
