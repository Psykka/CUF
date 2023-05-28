using Microsoft.AspNetCore.Mvc;
using CUF.Models;
using CUF.Data;
using Microsoft.EntityFrameworkCore;

namespace CUF.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext db;

    public ProductController(AppDbContext db)
    {
        this.db = db;
    }

    public IActionResult List()
    {
        return View(db.Products?
            .Take(50)
            .ToList()
            .OrderBy(p => p.Name));
    }

    public IActionResult Register(ProductModel data)
    {
        if (data.Id != 0)
        {
            data = db.Products?.Find(data.Id);
        }

        return View(data);
    }

    [HttpPost]
    public IActionResult Save(ProductModel data)
    {
        if (data.Id == 0)
        {
            // TODO: Add CreatedBy
            // TODO: Add UpdatedBy
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            db.Products?.Add(data);
        }
        else
        {
            data.UpdatedAt = DateTime.Now;
            db.Products?.Update(data);
        }

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return View("Error", new ErrorViewModel { RequestId = ex.Message });
        }

        return RedirectToAction("List", "Product");
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Search(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }

        var products = await db.Products?
            .Where(p => p.Name.ToLower().Contains(name))
            .Take(10)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await db.Products?
            .Take(50)
            .ToListAsync();

        return Ok(product);
    }
}