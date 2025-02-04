using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 3 - Roles
/// </summary>
[Route("Task03")]
[ApiController]
public class Task03Controller(ILogger<Task03Controller> logger)
    : Controller
{
    [AllowAnonymous]
    [HttpGet("GetClaims")]
    public IActionResult GetClaims()
    {
        logger.LogInformation("Returning user claims");
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
            Claims = User.Claims.Select(x => new { x.Type, x.Value })
        });
    }
}