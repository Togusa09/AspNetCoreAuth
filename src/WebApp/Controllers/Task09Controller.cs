using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorisation;
using WebApp.Models;

namespace WebApp.Controllers;

/// <summary>
/// Controller for Task 09 - Resource based authorization
/// </summary>
/// <param name="authorizationService"></param>
/// <param name="allCraft"></param>
[Route("Task09")]
[ApiController]
public class Task09Controller(
    IAuthorizationService authorizationService,
    ILogger<Task09Controller> logger,
    Craft[] allCraft
) : Controller
{
    [Authorize]
    [HttpGet("Craft")]
    public IActionResult GetCraftList()
    {
        logger.LogInformation("Returning list of available craft");
        return Json(allCraft);
    }

    [AllowAnonymous]
    [HttpGet("Craft/{craftName}")]
    public async Task<IActionResult> GetCraft(string craftName)
    {
        logger.LogInformation("User querying information for {craft}", craftName);

        var selectedCraft = allCraft.FirstOrDefault(c => c.Name == craftName);
        if (selectedCraft == null)
        {
            logger.LogWarning("{craft} could not be found", craftName);
            return NotFound();
        }

        var authorizationResult = await authorizationService
            .AuthorizeAsync(User, selectedCraft, new IsCertifiedForCraftRequirement());

        if (!authorizationResult.Succeeded)
        {
            logger.LogWarning("User is not authorised to access {craft}", craftName);
            return Forbid();
        }

        logger.LogInformation("Returning information for {craft}", craftName);
        return Json(selectedCraft);
    }
}