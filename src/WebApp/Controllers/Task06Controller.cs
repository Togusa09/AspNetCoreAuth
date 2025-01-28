using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorisation;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Task 06 - Resource based authorization
    /// </summary>
    /// <param name="authorizationService"></param>
    /// <param name="allCraft"></param>
    [Route("Task06")]
    [ApiController]
    public class Task06Controller(
        IAuthorizationService authorizationService,
        Craft[] allCraft
        ) : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            // View should already exist in workshop assets
            return View();
        }
        
        [Authorize]
        [HttpGet("Craft")]
        public IActionResult CapeCanaveralVehicles()
        {
            return Json(new[]
            {
                Craft.Mercury,
                Craft.Gemini,
                Craft.Apollo,
                Craft.Shuttle
            });
        }

        [AllowAnonymous]
        [HttpGet("Craft/{craftName}")]
        public async Task<IActionResult> GetCraft(string craftName)
        {
            var selectedCraft = allCraft.FirstOrDefault(c => c.Name == craftName);
            if (selectedCraft == null)
            {
                return NotFound();
            }

            var authorizationResult = await authorizationService
                .AuthorizeAsync(User, selectedCraft, new IsCertifiedForCraftRequirement());

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Json(selectedCraft);

        }
    }
}
