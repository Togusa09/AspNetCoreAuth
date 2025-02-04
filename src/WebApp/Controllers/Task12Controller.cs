using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 9 - JWT authentication
/// </summary>
/// <param name="httpContextAccessor"></param>
[Route("Task12")]
[ApiController]
public class Task12Controller(
    IHttpContextAccessor httpContextAccessor,
    ILogger<Task12Controller> logger
) : Controller
{
    [Authorize]
    [HttpGet("GetUserJwt")]
    public async Task<IActionResult> GetUserJwt()
    {
        logger.LogInformation("User attempting to retrieve JWT token");

        if (User.Identity!.AuthenticationType != "OIDC")
        {
            logger.LogWarning("User not authenticated with OIDC");
            return BadRequest("User needs to be logged in with OIDC to get token");
        }

        var token = await httpContextAccessor.HttpContext.GetTokenAsync("jwt_token");
        logger.LogInformation("Token successfully retrieved");
        return Json(token);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("TestJwt")]
    public IActionResult TestJwt()
    {
        logger.LogInformation("Testing JWT authentication");
        return Json("JWT worked successfully");
    }
}