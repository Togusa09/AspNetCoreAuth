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
            return View();
        }

        // Get User claims

        // Anonymous method
        [AllowAnonymous]
        [HttpGet("NoAuth")]
        public IActionResult TestAnonymous()
        {
            return OkResponse();
        }

        // Authenticated method
        [Authorize]
        [HttpGet("Auth")]
        public IActionResult TestAuthenticated()
        {
            return OkResponse();
        }

        // Role authorisation

        [Authorize(Roles = "Pilot")]
        [HttpGet("IsInRole/Pilot")]
        public IActionResult TestPilot()
        {
            return OkResponse();
        }

        [Authorize(Roles = "Engineer")]
        [HttpGet("IsInRole/Engineer")]
        public IActionResult TestEngineer()
        {
            return OkResponse();
        }

        private IActionResult OkResponse()
        {
            return Ok(new
            {
                Name = User.Identity?.Name ?? "Anonymous",
                AuthenticationType = User.Identity?.AuthenticationType ?? "None",
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            });
        }
    }
}
