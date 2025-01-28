using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Task 3 - Policies
    /// </summary>
    [Route("Task04")]
    [ApiController]
    public class Task04Controller : Controller
    {
        [Authorize(Policy = "FlightDirector")]
        [HttpGet("MissionControl")]
        public IActionResult CapeCanaveralMissionControl()
        {
            return Json("You are in mission control");
        }

        [Authorize(Policy = "Astronaut")]
        [HttpGet("LaunchPad")]
        public IActionResult CapeCanaveralLaunchPad()
        {
            return Json("You are on the launchpad");
        }
    }
}
