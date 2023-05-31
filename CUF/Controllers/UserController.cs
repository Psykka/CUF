using Microsoft.AspNetCore.Mvc;
using CUF.Data;
using CUF.Models;
using CUF.Utils;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> LandRegister(UserModel user)
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
        Save(user);

        // create session
        await CreateSession(user);

        // redirect to SupplierController List
        return RedirectToAction("List", "Supplier");
    }

    public int Save(UserModel user)
    {
        user.Password = PasswordService.SHA512(user.Password);
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        db.Users.Add(user);

        return db.SaveChanges();
    }

    public IActionResult Register(UserModel data)
    {
        if (data.Id != 0)
        {
            data = db.Users?.Find(data.Id);
        }

        return View(data);
    }
    

    [HttpPost]
    public async Task<IActionResult> AdminRegister(UserModel user)
    {
        if (user.Email == null || user.Password == null || user.Username == null)
        {
            TempData["Error"] = "Email, Nome de usuário e Senha são obrigatórios";
            return RedirectToAction("Register", "User", user);
        }

        var userDb = db.Users?.FirstOrDefault(u => u.Email == user.Email);

        // validate user data
        if (userDb != null)
        {
            TempData["Error"] = "Email já cadastrado";
            return RedirectToAction("Register", "User", user);
        }

        // save to new user to database
        Save(user);

        // create session
        await CreateSession(user);

        // redirect to SupplierController List
        return RedirectToAction("List", "User");
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
            new Claim("UserId", user.Id.ToString()),
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

    public IActionResult List()
    {
        return View(db.Users?
            .Take(50)
            .ToList()
            .OrderBy(p => p.Username));
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Search(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }

        var products = await db.Users?
            .Where(p => p.Username.Contains(name))
            .Take(10)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await db.Users?
            .Take(50)
            .ToListAsync();

        return Ok(product);
    }
}

