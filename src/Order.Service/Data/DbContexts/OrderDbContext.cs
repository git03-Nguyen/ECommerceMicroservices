using Microsoft.EntityFrameworkCore;
using Order.Service.Data.Models;

namespace Order.Service.Data.DbContexts;

public class OrderDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public OrderDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("OrderDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Order>().ToTable("Orders");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
    }
}