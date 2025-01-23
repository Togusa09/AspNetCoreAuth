using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication.CustomScheme;

namespace WebApp.Controllers;

public class CustomAuthSchemeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        // View from workshop assets
        return View();
    }

    [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
    public IActionResult GetData()
    {
        return Json(new
        {
            Data1 = "asdasfsfdsd",
            Data2 = "12e23rwefesf",
            User = User.Identity!.Name
        });
    }
}