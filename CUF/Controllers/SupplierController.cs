using CUF.Data;
using CUF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CUF.Controllers;

[Authorize]
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

    public IActionResult Register(SupplierModel data)
    {
        if (data.Id != 0)
        {
            data = db.Suppliers?.Find(data.Id);
        }

        return View(data);
    }

    [HttpPost]
    public IActionResult Save(SupplierModel data)
    {
        if (data.Id == 0)
        {
            // TODO: Add CreatedBy
            // TODO: Add UpdatedBy
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            db.Suppliers?.Add(data);
        }
        else
        {
            data.UpdatedAt = DateTime.Now;
            db.Suppliers?.Update(data);
        }

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return View("Error", new ErrorViewModel { RequestId = ex.Message });
        }

        return RedirectToAction("List", "Supplier");
    }
}

