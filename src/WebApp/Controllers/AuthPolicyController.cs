using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthPolicyController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            // View should already exist in workshop assets
            return View();
        }

        [Authorize(Policy = "FlightDirector")]
        [HttpGet("AuthPolicy/MissionControl")]
        public IActionResult CapeCanaveralMissionControl()
        {
            return Json("You are in mission control");
        }

        [Authorize(Policy = "Astronaut")]
        [HttpGet("AuthPolicy/LaunchPad")]
        public IActionResult CapeCanaveralLaunchPad()
        {
            return Json("You are on the launchpad");
        }
    }
}
