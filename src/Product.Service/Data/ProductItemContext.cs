using Microsoft.EntityFrameworkCore;
using Product.Service.Models;

namespace Product.Service.Data;

public class ProductItemContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public ProductItemContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<ProductItem?> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("ProductItemDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ProductItemContextSeed.Seed(modelBuilder);
    }
}