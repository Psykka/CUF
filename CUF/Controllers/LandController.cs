using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CUF.Models;

namespace CUF.Controllers;

public class LandController : Controller
{
    private readonly ILogger<LandController> _logger;

    public LandController(ILogger<LandController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        // check if user is already logged in
        var claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
        {
            return RedirectToAction("List", "Supplier");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}