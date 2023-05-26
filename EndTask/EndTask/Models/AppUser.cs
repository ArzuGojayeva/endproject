using Microsoft.AspNetCore.Identity;

namespace EndTask.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
    }
}
