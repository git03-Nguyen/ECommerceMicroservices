using Catalog.Service.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.DbContexts;

public class CatalogDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("CatalogDb"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CatalogDbContextSeeds.Seed(modelBuilder);
    }
    
    
}