using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Data;

public class CustomerContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public CustomerContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Models.Customer?> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("CustomerDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CustomerContextSeed.Seed(modelBuilder);
    }
}