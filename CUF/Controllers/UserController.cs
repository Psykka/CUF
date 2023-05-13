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
    public async Task<IActionResult> Register(UserModel user)
    {
        if (user.Email == null || user.Password == null || user.Username == null)
        {
            TempData["Error"] = "Email, Nome de usuário e Senha são obrigatórios";
            return RedirectToAction("Index", "Land");
        }

        var userDb = db.Users?.FirstOrDefault(u => u.Email == user.Email);

        // validate user data
        if (userDb != null)
        {
            TempData["Error"] = "Email já cadastrado";
            return RedirectToAction("Index", "Land");
        }

        // save to new user to database
        user.Password = PasswordService.SHA512(user.Password);
        db.Users.Add(user);
        db.SaveChanges();

        // create session
        await CreateSession(user);

        // redirect to SupplierController List
        return RedirectToAction("List", "Supplier");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Land");
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

