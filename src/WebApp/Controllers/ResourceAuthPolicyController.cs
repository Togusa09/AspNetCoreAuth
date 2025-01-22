using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorisation;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ResourceAuthPolicyController(
        IAuthorizationService authorizationService,
        Craft[] allCraft
        ) : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        [HttpGet("ResourceAuthPolicy/Craft")]
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
        [HttpGet("ResourceAuthPolicy/Craft/{craftName}")]
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
