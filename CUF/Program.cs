using Microsoft.EntityFrameworkCore;
using CUF.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var connecitonStringMysql = builder.Configuration.GetConnectionString("ConnctionMysql");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(
    connecitonStringMysql,
    // MariaDB 10.4.21 used by xampp
    // On docker mariadb:10.4.21
    ServerVersion.Parse("10.4.21-MariaDB")
));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Land/Login";
        options.AccessDeniedPath = "/Land/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Land}/{action=Index}/{id?}");

app.Run();