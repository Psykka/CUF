using Microsoft.EntityFrameworkCore;
using CUF.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connecitonStringMysql = builder.Configuration.GetConnectionString("ConnctionMysql");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(
    connecitonStringMysql,
    ServerVersion.Parse("10.4.21-MariaDB")
));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Land}/{action=Index}/{id?}");

app.Run();