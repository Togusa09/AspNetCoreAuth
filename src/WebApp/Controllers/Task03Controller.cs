using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Task 3 - Roles
    /// </summary>
    [Route("Task03")]
    [ApiController]
    public class Task03Controller : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            // View should already exist in workshop assets
            return View();
        }

        [AllowAnonymous]
        [HttpGet("GetClaims")]
        public IActionResult GetClaims()
        {
            return OkResponse("Get Claims");
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
