using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

/// <summary>
/// Controller for task 2 - Cookie Authentication
/// </summary>
[Route("Task02")]
[ApiController]
public class Task02Controller(ILogger<Task02Controller> logger)
    : Controller
{
    [Authorize]
    [HttpGet("Authenticated")]
    public IActionResult Authenticated()
    {
        logger.LogInformation("Accessing authenticate endpoint with user {name}", User.Identity!.Name);
        return Json("Authenticated Request");
    }

    [AllowAnonymous]
    [HttpGet("Anonymous")]
    public IActionResult NotAuthenticated()
    {
        logger.LogInformation("Accessing anonymous endpoint with user {name}", User.Identity!.Name);
        return Json("Not Authenticated Request");
    }
}