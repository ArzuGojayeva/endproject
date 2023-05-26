using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.DAL;
using Task.Models;
using Task.ViewModels.Account;

namespace Task.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;

        private readonly SignInManager<AppUser> _signinmanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public AccountController(SignInManager<AppUser> signinmanager, RoleManager<IdentityRole> rolemanager, UserManager<AppUser> usermanager)
        {
            _signinmanager = signinmanager;
            _rolemanager = rolemanager;
            _usermanager = usermanager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser newuser = new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
            };
            IdentityResult result = await _usermanager.CreateAsync(newuser, registerVM.Password);
            if (result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
            }
            await _signinmanager.SignInAsync(newuser, false);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) { return View(); }
            if (loginVM == null) { return View(); }
            var user = await _usermanager.FindByNameAsync(loginVM.UserName);
            var result = await _signinmanager.PasswordSignInAsync(user, loginVM.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
