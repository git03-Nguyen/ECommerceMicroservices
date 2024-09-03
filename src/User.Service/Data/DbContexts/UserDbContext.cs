using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User.Service.Data.Models;
using User.Service.Options;

namespace User.Service.Data.DbContexts;

public class UserDbContext : DbContext
{
    private readonly IOptions<UserDbOptions> _dbOptions;

    public UserDbContext(DbContextOptions<UserDbContext> options, IOptions<UserDbOptions> dbOptions) : base(options)
    {
        _dbOptions = dbOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_dbOptions.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Seller>().ToTable("Sellers");
    }
}