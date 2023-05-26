using Microsoft.AspNetCore.Identity;

namespace _25may.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
    }
}
