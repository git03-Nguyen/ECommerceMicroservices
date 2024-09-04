using Catalog.Service.Data.Models;
using Catalog.Service.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Data.DbContexts;

public class CatalogDbContext : DbContext
{
    private readonly IOptions<CatalogDbOptions> _dbOptions;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<CatalogDbOptions> dbOptions) : base(options)
    {
        _dbOptions = dbOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_dbOptions.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CatalogDbContextSeeds.Seed(modelBuilder);
    }
}