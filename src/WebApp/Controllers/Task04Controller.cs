using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 4 - Roles
/// </summary>
[Route("Task04")]
[ApiController]
public class Task04Controller(ILogger<Task04Controller> logger)
    : Controller
{
    [Authorize(Roles = "FlightDirector")]
    [HttpGet("MissionControl")]
    public IActionResult MissionControl()
    {
        logger.LogInformation("User is in mission control");
        return Json("You are in mission control");
    }

    [Authorize(Roles = "Pilot")]
    [HttpGet("LaunchPad")]
    public IActionResult LaunchPad()
    {
        logger.LogInformation("User is on the launchpad");
        return Json("You are on the launchpad");
    }
}