using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Task 9 - JWT authentication
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    [Route("Task09")]
    [ApiController]
    public class Task09Controller(IHttpContextAccessor httpContextAccessor) : Controller
    {

        [Authorize]
        [HttpGet("GetUserJwt")]
        public async Task<IActionResult> GetUserJwt()
        {
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("jwt_token");
            return Json(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("TestJwt")]
        public IActionResult TestJwt()
        {
            return Json("JWT worked successfully");
        }
    }
}
