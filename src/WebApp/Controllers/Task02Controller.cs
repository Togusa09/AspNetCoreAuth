using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for task 2 - Cookie Authentication
    /// </summary>
    [Route("Task02")]
    [ApiController]
    public class Task02Controller : Controller
    {
        [Authorize]
        [HttpGet("Authenticated")]
        public IActionResult Authenticated()
        {
            return Json("Authenticated Request");
        }

        [AllowAnonymous]
        [HttpGet("Anonymous")]
        public IActionResult NotAuthenticated()
        {
            return Json("Not Authenticated Request");
        }
    }
}
