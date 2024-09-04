using Basket.Service.Data.Models;
using Basket.Service.Options;
using Contracts.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Basket.Service.Data;

public class BasketDbContext : DbContext
{
    private readonly IOptions<BasketDbOptions> _dbOptions;

    public BasketDbContext(DbContextOptions<BasketDbContext> options, IOptions<BasketDbOptions> dbOptions) : base(options)
    {
        _dbOptions = dbOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Models.Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_dbOptions.Value.ConnectionString)
            .AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Basket>().ToTable(nameof(Baskets));
        modelBuilder.Entity<BasketItem>().ToTable(nameof(BasketItems));
    }
}