using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 7 - Policies
/// </summary>
[Route("Task07")]
[ApiController]
public class Task07Controller(ILogger<Task07Controller> logger)
    : Controller
{
    [Authorize(Policy = "FlightDirector")]
    [HttpGet("MissionControl")]
    public IActionResult CapeCanaveralMissionControl()
    {
        logger.LogInformation("User is in mission control");
        return Json("You are in mission control");
    }

    [Authorize(Policy = "Astronaut")]
    [HttpGet("LaunchPad")]
    public IActionResult CapeCanaveralLaunchPad()
    {
        logger.LogInformation("User is on the launchpad");
        return Json("You are on the launchpad");
    }
}