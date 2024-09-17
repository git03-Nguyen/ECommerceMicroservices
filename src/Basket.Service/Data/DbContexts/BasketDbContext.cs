using Basket.Service.Data.Models;
using Basket.Service.Options;
using Contracts.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Basket.Service.Data.DbContexts;

public class BasketDbContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public BasketDbContext(DbContextOptions<BasketDbContext> options, IOptions<DatabaseOptions> databaseOptions) :
        base(options)
    {
        _databaseOptions = databaseOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Models.Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Product> Products { get; set; }
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

        // 1 basket - many basket items
        modelBuilder.Entity<Models.Basket>()
            .HasMany(x => x.BasketItems)
            .WithOne(x => x.Basket)
            .HasForeignKey(x => x.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1 product - many basket items
        modelBuilder.Entity<Product>()
            .HasMany(x => x.BasketItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1 seller - many products
        modelBuilder.Entity<Seller>()
            .HasMany(x => x.Products)
            .WithOne(x => x.Seller)
            .HasForeignKey(x => x.SellerId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutoInclude - BAD PRACTICES!!!!!!!!!!
        modelBuilder.Entity<Models.Basket>()
            .Navigation(x => x.BasketItems)
            .AutoInclude();

        modelBuilder.Entity<BasketItem>()
            .Navigation(x => x.Product)
            .AutoInclude();

        modelBuilder.Entity<Product>()
            .Navigation(x => x.Seller)
            .AutoInclude();
    }
}