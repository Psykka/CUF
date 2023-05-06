using Microsoft.AspNetCore.Mvc;
using CUF.Data;
using CUF.Models;
using CUF.Utils;

namespace CUF.Controllers;
public class UserController : Controller
{
    private readonly AppDbContext db;

    public UserController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpPost]
    public IActionResult Login(UserModel user)
    {
        // handle user login action
        if (user.Email == null || user.Password == null)
        {
            return RedirectToAction("Login", "Land", new { error = "Email and Password are required" });
        }

        var userDb = db.Users?.FirstOrDefault(u => u.Email == user.Email);

        if (userDb == null)
        {
            return RedirectToAction("Login", "Land", new { error = "Email or Password is incorrect" });
        }

        var passwordHash = PasswordService.SHA512(user.Password);

        if (passwordHash != userDb.Password)
        {
            return RedirectToAction("Login", "Land", new { error = "Email or Password is incorrect" });
        }

        // create session
        // redirect to SupplierController List
        return RedirectToAction("List", "Supplier");
    }

    [HttpPost]
    public IActionResult Register()
    {
        // handle user register
        // save to new user to database
        // create session
        // redirect to SupplierController List
        return RedirectToAction("List", "Supplier");
    }
}

