using CUF.Models;
using Microsoft.EntityFrameworkCore;

namespace CUF.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<UserModel>? Users { get; set; }
    
    public DbSet<SupplierModel>? Suppliers { get; set; }

    public DbSet<ProductModel>? Products { get; set; }

    public DbSet<AttachmentModel>? Attachment { get; set; }

    public DbSet<OrderModel>? Orders { get; set; }

    public DbSet<OrderInformationModel>? OrderInformation { get; set; }

    public DbSet<CategoryModel>? Categories { get; set; }
}