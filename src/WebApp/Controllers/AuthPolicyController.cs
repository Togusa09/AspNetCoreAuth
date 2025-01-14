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

        [Authorize(Policy = "TracyFamily")]
        [HttpGet("AuthPolicy/LaunchSite/TracyIsland")]
        public IActionResult TracyIslandLaunchSite()
        {
            return Json(new[]
            {
                Craft.ThunderBird1,
                Craft.ThunderBird3,
                Craft.ThunderBird5,
            });
        }

        [Authorize(Policy = "Astronaut")]
        [HttpGet("AuthPolicy/LaunchSite/CapeCanaveral")]
        public IActionResult CapeCanaveralLaunchSite()
        {
            return Json(new[]
            {
                Craft.Mercury,
            });
        }

        [AllowAnonymous]
        [HttpGet("AuthPolicy/LaunchSite/KennedySpaceCentre")]
        public IActionResult KennedySpaceCentreLaunchSite()
        {
            return Json(new[]
            {
                Craft.Apollo,
            });
        }
    }
}
