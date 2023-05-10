using CUF.Data;
using CUF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost]
    public IActionResult Save(SupplierModel data)
    {
        if (data.Id == 0)
        {
            db.Suppliers?.Add(data);
        }
        else
        {
            db.Entry<SupplierModel>(db.Suppliers.FirstOrDefault(s => s.Id == data.Id)).CurrentValues.SetValues(data);
        }

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException exc)
        {
            
        }

        return RedirectToAction("List", "Supplier");
    }
}

