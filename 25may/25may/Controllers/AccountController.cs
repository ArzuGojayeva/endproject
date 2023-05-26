using _25may.Models;
using _25may.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _25may.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signinmanager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager, RoleManager<IdentityRole> rolemanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _rolemanager = rolemanager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser newUser= new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
            };
            IdentityResult result=await _usermanager.CreateAsync(newUser,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach(var item in  result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            await _usermanager.AddToRoleAsync(newUser, "admin");
            await _signinmanager.SignInAsync(newUser, false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) { return View(); }
            var user=await _usermanager.FindByNameAsync(loginVM.UserName);
            if (user == null) return View();
            var result =await _signinmanager.PasswordSignInAsync(user, loginVM.Password, false, true);
            if (!result.Succeeded) {
                ModelState.AddModelError("", "error");
                return View();
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddRole()
        {
            await _rolemanager.CreateAsync(new IdentityRole { Name= "admin" });
            return NoContent();
        }
    }
}
