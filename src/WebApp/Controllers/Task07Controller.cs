using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication.CustomScheme;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 7 - Custom Auth Scheme
/// </summary>
[Route("Task07")]
[ApiController]
public class Task07Controller : Controller
{
    //[AllowAnonymous]
    //public IActionResult Index()
    //{
    //    // View from workshop assets
    //    return View();
    //}

    [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
    [HttpGet("GetData")]
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