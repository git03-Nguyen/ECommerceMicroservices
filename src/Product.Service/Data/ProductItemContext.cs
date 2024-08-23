using Microsoft.EntityFrameworkCore;

namespace Product.Service.Data;

public class ProductItemContext : DbContext
{
    protected readonly IConfiguration _configuration;
    
    public ProductItemContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Models.ProductItem?> Products { get; set; }
    
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