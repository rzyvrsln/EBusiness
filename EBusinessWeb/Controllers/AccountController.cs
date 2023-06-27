using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBusinessWeb.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View();

        [HttpGet]
        public async Task<IActionResult> SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM signUpVM)
        {
            if (!ModelState.IsValid) return View();
            var user = userManager.FindByEmailAsync(signUpVM.Email);
            if(user != null)
            {
                ModelState.AddModelError("Email", "This email already exist.");
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
            //await userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index", "Account");
        }
    }
}
