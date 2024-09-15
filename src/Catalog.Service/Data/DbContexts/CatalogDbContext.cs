using Catalog.Service.Data.Models;
using Catalog.Service.Options;
using Contracts.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Data.DbContexts;

public class CatalogDbContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<DatabaseOptions> databaseOptions) :
        base(options)
    {
        _databaseOptions = databaseOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_databaseOptions.Value.ConnectionString)
            .AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_databaseOptions.Value.SchemaName);

        // 1 category has many products
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1 seller has many products
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Seller)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SellerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Soft delete
        modelBuilder.Entity<Category>()
            .HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);

        // Auto include
        modelBuilder.Entity<Product>()
            .Navigation(p => p.Category)
            .AutoInclude();

        modelBuilder.Entity<Product>()
            .Navigation(p => p.Seller)
            .AutoInclude();

        // CatalogDbContextSeeds.Seed(modelBuilder);
    }
}