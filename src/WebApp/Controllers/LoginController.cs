using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class LoginController : Controller
{
    public async Task<IActionResult> Login(string name)
    {
        // This example is performed by a trained stunt developer. Please do not try this at home.
        var identity = new ClaimsIdentity(
        [
            new(ClaimTypes.Name, name)
        ], CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult LoginOidc(string returnUrl = "/")
    {
        var param = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };

        return Challenge(param, OpenIdConnectDefaults.AuthenticationScheme);
    }
}