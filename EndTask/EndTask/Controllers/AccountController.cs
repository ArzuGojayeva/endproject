using EndTask.Models;
using EndTask.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinmanager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinmanager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signinmanager = signinmanager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) { return View(); }
            if (registerVM == null) { return View(); }
            AppUser newUser=new AppUser()
            {
                UserName=registerVM.Name,
                Email=registerVM.Email,
            };
            IdentityResult result=await _userManager.CreateAsync(newUser,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors) {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(newUser, "admin");
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
            var user=await _userManager.FindByNameAsync(loginVM.UserName);
            if (user == null) { return View(); }
            var result=await _signinmanager.PasswordSignInAsync(user, loginVM.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "error");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddRole()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name= "admin" });
            return NoContent();
        }

    }

}
