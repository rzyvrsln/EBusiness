using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EBusinessWeb.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> userManager;
        SignInManager<AppUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login() => RedirectToAction(nameof(AdminSignIn));

        [HttpGet]
        public async Task<IActionResult> SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM signUpVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await userManager.FindByEmailAsync(signUpVM.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", "This email already exist.");
                return View();
            }
            var roles = await userManager.GetRolesAsync(user);

            if (roles[0] == "Admin")
            {
                ModelState.AddModelError("UserName", "This username already usin for Admin.");
                return View();
            }

            AppUser appUser = new AppUser
            {
                Name = signUpVM.Name,
                Surname = signUpVM.Surname,
                UserName = signUpVM.UserName,
                Email = signUpVM.Email
            };

            IdentityResult result = await userManager.CreateAsync(appUser, signUpVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }

            await userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInVM signInVM)
        {
            if (!ModelState.IsValid) return View();

            var user = await userManager.FindByNameAsync(signInVM.UserName);


            if (user is null)
            {
                ModelState.AddModelError("UserName", "This username not exist.");
                return View();
            }
            var roles = await userManager.GetRolesAsync(user);


            if (roles[0] == "Admin")
            {
                ModelState.AddModelError("UserName", "This username already usin for Admin.");
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(user, signInVM.Password, signInVM.IsParsistance,true);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password wrong. Try again.");
                return View();
            }

            return RedirectToActionPermanent("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AdminSignIn() => View();

        [HttpPost]
        public async Task<IActionResult> AdminSignIn(AdminSignIn adminSign)
        {
            if (!ModelState.IsValid) return View();
            var admin = await userManager.FindByNameAsync(adminSign.UserName);
            if (admin is null)
            {
                ModelState.AddModelError("UserName", "This username not exist.");
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(admin, adminSign.Password, adminSign.IsParsistance, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password wrong. Try again.");
                return View();
            }
            ;
            return RedirectToAction("Index", "Home", new { area = "Manage" });
        }

        //[HttpGet]
        //public async Task<IActionResult> AddRoles()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return View();
        //}
    }
}
