using Microsoft.AspNetCore.Mvc;
using CUF.Data;
using CUF.Models;
using CUF.Utils;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CUF.Controllers;
public class UserController : Controller
{
    private readonly AppDbContext db;

    public UserController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserModel user)
    {
        // handle user login action
        if (user.Email == null || user.Password == null)
        {
            TempData["Error"] = "Email and Password are required";
            return RedirectToAction("Login", "Land");
        }

        var userDb = db.Users?.FirstOrDefault(u => u.Email == user.Email);
        var passwordHash = PasswordService.SHA512(user.Password);

        if (userDb == null || passwordHash != userDb.Password)
        {
            TempData["Error"] = "Email or Password is incorrect";
            return RedirectToAction("Login", "Land");
        }

        // create session
        await CreateSession(userDb);

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

    Task CreateSession(UserModel user)
    {
        // create session
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var properties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
        };

        return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
    }
}

