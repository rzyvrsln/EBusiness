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
        public async Task<IActionResult> AccessDenied() => RedirectToAction(nameof(AdminSignIn));
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
                var roles = await userManager.GetRolesAsync(user);

                if (roles[0] == "Admin")
                {
                    ModelState.AddModelError("UserName", "This username already usin for Admin.");
                    return View();
                }

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

            await userManager.AddToRoleAsync(appUser, "User");
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

            var result = await signInManager.PasswordSignInAsync(user, signInVM.Password, signInVM.IsParsistance, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password wrong. Try again.");
                return View();
            }

            return RedirectToActionPermanent("Index", "Home");
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

        #region SocialMediaOperations

        [HttpGet]
        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new { returnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> SocialMediaResponse(string returnUrl)
        {
            var loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction(nameof(SignUp));
            else
            {
                var result = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                if (result.Succeeded)
                    return Redirect(returnUrl);
                else
                {
                    if (loginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        AppUser user = new AppUser()
                        {
                            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name)
                        };

                        var createResult = await userManager.CreateAsync(user);
                        if(createResult.Succeeded)
                        {
                            var identityLogin = await userManager.AddLoginAsync(user, loginInfo);
                            if(identityLogin.Succeeded)
                            {
                                await signInManager.SignInAsync(user, isPersistent: true);
                                return Redirect(nameof(SignIn));
                            }
                        }
                    }
                }
            }
            return RedirectToAction(nameof(SignUp));
        }

        #endregion
    }
}
