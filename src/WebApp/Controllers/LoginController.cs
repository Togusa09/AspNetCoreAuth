using System.Security.Claims;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers;

/// <summary>
/// Controller for task 1 and ? for managing authentication
/// </summary>
public class LoginController
    (ILogger<LoginController> logger)
    : Controller
{
    public async Task<IActionResult> Login(string name, string familyName)
    {
        logger.LogInformation("Logging in {name} {surname} with cookie auth", name, familyName);

        // Populate the identity with the passed in details
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

        // This example is performed by a trained stunt developer. Please do not try this at home.
        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        logger.LogInformation("Logging out user {name}", User.Identity!.Name);
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult LoginOidc(string returnUrl = "/")
    {
        var param = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };

        logger.LogInformation("Redirecting login to OIDC challenge");
        return Challenge(param, OpenIdConnectDefaults.AuthenticationScheme);
    }
}