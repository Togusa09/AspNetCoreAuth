using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    // Based on https://hajekj.net/2017/03/06/forcing-reauthentication-with-azure-ad/

    public class SensitiveController(IHttpContextAccessor httpContextAccessor) : Controller
    {
        [RequireReauthentication(20)]
        public async Task<IActionResult> Index()
        {
            var state = new Dictionary<string, string> { { "reauthenticate", "true" } };

            await httpContextAccessor.HttpContext.ChallengeAsync(
                OpenIdConnectDefaults.AuthenticationScheme, 
                new AuthenticationProperties(state)
                {
                    RedirectUri = "/SensitivePage"
                }
            );


            // Not sure if we should be reaching here cause of the challenge?

            // View should already exist in workshop assets
            return View();
        }

        [RequireReauthentication(120)]
        public IActionResult SensitivePage()
        {
            // View should already exist in workshop assets
            return View();
        }
    }
}
