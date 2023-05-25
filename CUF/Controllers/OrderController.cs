using Microsoft.AspNetCore.Mvc;
using CUF.Models;
using CUF.Data;
using Microsoft.EntityFrameworkCore;

namespace CUF.Controllers;

public class OrderController : Controller
{
    private readonly AppDbContext db;

    public OrderController(AppDbContext db)
    {
        this.db = db;
    }

    public IActionResult List()
    {
        return View(db.Orders?
            .Take(50)
            .ToList());
    }

    public IActionResult Register(OrderModel data)
    {
        if (data.Id != 0)
        {
            data = db.Orders?.Find(data.Id);
        }

        return View(data);
    }

    [HttpPost]
    public IActionResult Save(OrderModel data)
    {
        if (data.Id == 0)
        {
            // TODO: Add CreatedBy
            // TODO: Add UpdatedBy
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            db.Orders?.Add(data);
        }
        else
        {
            data.UpdatedAt = DateTime.Now;
            db.Orders?.Update(data);
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
}