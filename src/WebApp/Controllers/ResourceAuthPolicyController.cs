using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorisation;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ResourceAuthPolicyController(IAuthorizationService authorizationService) : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = "Astronaut")]
        [AllowAnonymous]
        [HttpGet("ResourceAuthPolicy/Craft/{craftName}")]
        public async Task<IActionResult> GetCraft(string craftName)
        {
            var authorizationResult = await authorizationService
                .AuthorizeAsync(User, new Craft(craftName), new IsCertifiedForCraftRequirement());

            if (authorizationResult.Succeeded)
            {
                return Json(new Craft(craftName));
            }

            return Forbid();
        }
    }
}
