using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class JwtAuthController(IHttpContextAccessor httpContextAccessor) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("JwtAuth/GetUserJwt")]
        public async Task<IActionResult> GetUserJwt()
        {
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("jwt_token");
            return Json(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("JwtAuth/TestJwt")]
        public IActionResult TestJwt()
        {
            return Json("JWT worked successfully");
        }
    }
}
