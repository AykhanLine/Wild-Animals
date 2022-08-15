using Heroes.Areas.Dashboard.DTOs;
using Heroes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : Controller
    {
       
        private readonly UserManager<User> _userManager; 
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return View();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            User user = new()
            {
                UserName = model.Email, //bunu hokman yazmaliyig
                FirstName = model.Firstame,
                LastName = model.Lastame,
                Email = model.Email,
                Fullname = model.Firstame + "" + model.Lastame

            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        [HttpPost] // YASMASAG DA OLAR
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
