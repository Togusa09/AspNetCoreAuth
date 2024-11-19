using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication.CustomScheme;

namespace WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
    public class CustomAuthSchemeController : Controller
    {
        [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
        public IActionResult GetData()
        {
            return Json(new
            {
                Data1 = "asdasfsfdsd",
                Data2 = "12e23rwefesf",
                User = User.Identity!.Name
            });
        }
    }
}
