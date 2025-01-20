using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class LoginController : Controller
{
    public async Task<IActionResult> Login(string name, string familyName)
    {
        // This example is performed by a trained stunt developer. Please do not try this at home.
        var identity = new ClaimsIdentity(
        [
            new(ClaimTypes.Name, name),
            new (ClaimTypes.Surname, familyName)

        ], CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaims(
        [
            new Claim(ClaimTypes.Role, "Pilot"),
            new Claim("craft", Craft.Mercury.Name)
        ]);

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