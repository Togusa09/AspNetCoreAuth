using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("~/{pathName}")]
    [AllowAnonymous]
    public IActionResult SharedIndex(string pathName)
    {
        logger.LogInformation("Directing to index for controller {controller}", pathName);
        return View($"~/Views/{pathName}/Index.cshtml");
    }
}
