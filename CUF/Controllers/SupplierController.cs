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
    [Produces("application/json")]
    public IActionResult Save(SupplierModel data)
    {
        if (data.Id == 0)
        {
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
            return Json(new { error = ex.InnerException?.Message });
        }

        return Json(new { success = true });
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Search(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }

        var products = await db.Suppliers?
            .Where(p => p.Trade.Contains(name) || p.Company.Contains(name))
            .Take(10)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await db.Suppliers?
            .Take(50)
            .ToListAsync();

        return Ok(product);
    }
}

