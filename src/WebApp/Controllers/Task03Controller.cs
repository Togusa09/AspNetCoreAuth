using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(new
        {
            // Name of user
            Name = User.Identity?.Name ?? "Anonymous",
            // Action being called
            Action = "Get Claims",
            // Authentication method used by user
            AuthenticationType = User.Identity?.AuthenticationType ?? "None",
            // Whether user is authenticated
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            // Claims possessed by the user
            Claims = User.Claims.Select(x => new { x.Type, x.Value })
        });
    }
}