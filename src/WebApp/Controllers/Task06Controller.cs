using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorisation;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 06 - prompt re-auth for sensitive content
/// </summary>
/// <param name="httpContextAccessor"></param>
[Route("Task06")]
[ApiController]
public class Task06Controller(
    IHttpContextAccessor httpContextAccessor,
    ILogger<Task06Controller> logger)
    : Controller
{
    // Based on https://hajekj.net/2017/03/06/forcing-reauthentication-with-azure-ad/

    [RequireReauthentication(20)]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var state = new Dictionary<string, string> { { "reauthenticate", "true" } };

        logger.LogInformation("Triggering authentication re-challenge");
        await httpContextAccessor.HttpContext.ChallengeAsync(
            OpenIdConnectDefaults.AuthenticationScheme,
            new AuthenticationProperties(state)
            {
                RedirectUri = "Task06/SensitivePage"
            }
        );


        // Not sure if we should be reaching here cause of the challenge?
        return View(); // View included in workshop assets
    }

    [RequireReauthentication(120)]
    [HttpGet("SensitivePage")]
    public IActionResult SensitivePage()
    {
        return View(); // View included in workshop assets
    }
}