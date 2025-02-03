using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication.CustomScheme;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 7 - Custom Auth Scheme
/// </summary>
[Route("Task07")]
[ApiController]
public class Task07Controller(ILogger<Task07Controller> logger)
    : Controller
{
    [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
    [HttpGet("GetData")]
    public IActionResult GetData()
    {
        logger.LogInformation("Retrieving data object for custom auth");
        return Json(new
        {
            Data1 = "asdasfsfdsd",
            Data2 = "12e23rwefesf",
            User = User.Identity!.Name
        });
    }
}