using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Order.Service.Data.Models;
using Order.Service.Options;

namespace Order.Service.Data.DbContexts;

public class OrderDbContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IOptions<DatabaseOptions> databaseOptions) :
        base(options)
    {
        _databaseOptions = databaseOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_databaseOptions.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_databaseOptions.Value.SchemaName);

        modelBuilder.Entity<Models.Order>()
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Order>()
            .Navigation(x => x.OrderItems)
            .AutoInclude();

        modelBuilder.Entity<OrderItem>()
            .HasKey(x => x.OrderItemId);
    }
}