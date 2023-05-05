using Microsoft.AspNetCore.Mvc;

namespace CUF.Controllers;

public class SupplierController : Controller
{
    public IActionResult List()
    {
        return View();
    }
}

