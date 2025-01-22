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
            return View();
        }

        // TODO: Need something to differentiate the policy here - Everyone is astronauts. Maybe actor or pilot?

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

        //[Authorize]
        //[HttpGet("AuthPolicy/CapeCanaveral/Vehicles")]
        //public IActionResult CapeCanaveralVehicles()
        //{
        //    return Json(new[]
        //    {
        //        Craft.Mercury,
        //    });
        //}

        //[AllowAnonymous]
        //[HttpGet("AuthPolicy/KennedySpaceCentre/Vehicles")]
        //public IActionResult KennedySpaceCentreVehicles()
        //{
        //    return Json(new[]
        //    {
        //        Craft.Apollo,
        //    });
        //}
    }
}
