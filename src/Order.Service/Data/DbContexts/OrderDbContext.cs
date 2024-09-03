using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Order.Service.Data.Models;
using Order.Service.Options;

namespace Order.Service.Data.DbContexts;

public class OrderDbContext : DbContext
{
    private readonly IOptions<OrderDbOptions> _dbOptions;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IOptions<OrderDbOptions> dbOptions) : base(options)
    {
        _dbOptions = dbOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_dbOptions.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Order>().ToTable(nameof(Orders));
        modelBuilder.Entity<OrderItem>().ToTable(nameof(OrderItems));
    }
}