using Microsoft.AspNetCore.Identity;

namespace Task.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
    }
}
