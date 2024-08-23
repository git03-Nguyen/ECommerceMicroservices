using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Data;

public class CustomerContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public CustomerContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<Models.Customer?> Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("CustomerDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CustomerContextSeed.Seed(modelBuilder);

    }

}