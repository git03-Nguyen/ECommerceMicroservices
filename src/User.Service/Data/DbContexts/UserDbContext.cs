using Contracts.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User.Service.Data.Models;
using User.Service.Options;

namespace User.Service.Data.DbContexts;

public class UserDbContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public UserDbContext(DbContextOptions<UserDbContext> options, IOptions<DatabaseOptions> databaseOptions) : base(options)
    {
        _databaseOptions = databaseOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
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
        
        modelBuilder.Entity<Customer>()
            .HasQueryFilter(x => !x.IsDeleted)
            .HasOne(c => c.Account)
            .WithOne()
            .HasForeignKey<Customer>(c => c.AccountId);

        modelBuilder.Entity<Seller>()
            .HasQueryFilter(x => !x.IsDeleted)
            .HasOne(c => c.Account)
            .WithOne()
            .HasForeignKey<Seller>(s => s.AccountId);

        modelBuilder.Entity<Customer>()
            .Navigation(c => c.Account)
            .AutoInclude();

        modelBuilder.Entity<Seller>()
            .Navigation(c => c.Account)
            .AutoInclude();
    }
}