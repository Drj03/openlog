using ADOTest.ViewModels;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADOTest.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        // private readonly SignInManager;
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            var redirectUrl = Url.Action("GoogleCallback", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

       // [HttpGet]
        public async Task<IActionResult> GoogleCallback(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync();
            if (!authenticateResult.Succeeded)
            {
                // Handle the case where authentication failed
                return RedirectToAction("Login");
            }

            // Perform any necessary actions with the authenticated user
            // For example, sign in the user or create a new account

            return RedirectToAction("Index", "Home");
        }
    }
}
