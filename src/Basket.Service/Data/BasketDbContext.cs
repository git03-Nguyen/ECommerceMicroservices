using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Data;

public class BasketDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public BasketDbContext(DbContextOptions<BasketDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<Models.Basket> Baskets { get; set; }
    public DbSet<Models.BasketItem> BasketItems { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("BasketDb"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Basket>().ToTable("Baskets");
        modelBuilder.Entity<Models.BasketItem>().ToTable("BasketItems");
    }
    
}