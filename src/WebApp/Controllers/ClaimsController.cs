using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("Claims")]
    [ApiController]
    public class ClaimsController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            // View should already exist in workshop assets
            return View();
        }

        [AllowAnonymous]
        [HttpGet("UserInfo")]
        public IActionResult TestAnonymous()
        {
            return OkResponse("Get User Info");
        }

        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult TestAuth()
        {
            return OkResponse("Test Auth");
        }

        // Role authorisation

        [Authorize(Roles = "Pilot")]
        [HttpGet("IsInRole/Pilot")]
        public IActionResult TestPilot()
        {
            return OkResponse("Pilot Role");
        }

        [Authorize(Roles = "FlightDirector")]
        [HttpGet("IsInRole/FlightDirector")]
        public IActionResult TestEngineer()
        {
            return OkResponse("Flight Director Role");
        }

        private IActionResult OkResponse(string action)
        {
            return Ok(new
            {
                Name = User.Identity?.Name ?? "Anonymous",
                Action = action,
                AuthenticationType = User.Identity?.AuthenticationType ?? "None",
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                Claims = User.Claims.Select(x => new {x.Type, x.Value})
            });
        }
    }
}
