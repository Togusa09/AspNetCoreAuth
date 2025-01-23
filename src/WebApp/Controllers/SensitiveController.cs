
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            return Ok("Message goes here");
        }

        [RequireReauthentication(120)]
        public IActionResult SensitivePage()
        {
            return View();
        }
    }

    public class RequireReauthenticationAttribute : Attribute, IAsyncResourceFilter
    {
        private int _timeElapsedSinceLast;
        public RequireReauthenticationAttribute(int timeElapsedSinceLast)
        {
            _timeElapsedSinceLast = timeElapsedSinceLast;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var foundAuthTime = int.TryParse(context.HttpContext.User.FindFirst("auth_time")?.Value, out int authTime);

            DateTime currentTime = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            var timeElapsed = unixTime - authTime;
            Debug.WriteLine($"Time elapsed: {timeElapsed} Required: {_timeElapsedSinceLast}");

            if (foundAuthTime && timeElapsed < _timeElapsedSinceLast)
            {
                await next();
            }
            else
            {
                var state = new Dictionary<string, string> { { "reauthenticate", "true" } };
                await context.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties(state)
                {
                    RedirectUri = context.HttpContext.Request.Path
                });
            }
        }
    }
}
