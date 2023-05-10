using CUF.Data;
using Microsoft.AspNetCore.Mvc;

namespace CUF.Controllers;

public class SupplierController : Controller
{

    private readonly AppDbContext db;

    public SupplierController(AppDbContext db)
    {
        this.db = db;
    }

    public IActionResult List()
    {
        return View(db.Suppliers?
            .Take(50)
            .ToList()
            .OrderBy(s => s.Trade != null ? s.Trade : s.Company));
    }

    public IActionResult Create()
    {
        return View();
    }
}

