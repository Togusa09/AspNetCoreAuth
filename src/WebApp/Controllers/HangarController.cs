using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class CapeCanaveralController : Controller
{
    [AllowAnonymous]
    public IActionResult KennedySpaceCenter()
    {
        return Ok();
    }

    [Authorize(Policy = "Astronaut")]
    public IActionResult SpaceForceStation()
    {
        return Ok();
    }
}

[Authorize]
public class TracyIslandController : Controller
{
    [AllowAnonymous]
    public IActionResult House()
    {
        return Ok();
    }

    [Authorize(Policy = "Astronaut")]
    [Authorize(Policy = "TracyFamily")]
    public IActionResult Hangar()
    {
        return Ok();
    }
}