using Contracts.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User.Service.Options;

namespace User.Service.Data.DbContexts;

public class UserDbContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public UserDbContext(DbContextOptions<UserDbContext> options, IOptions<DatabaseOptions> databaseOptions) :
        base(options)
    {
        _databaseOptions = databaseOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<Models.User> Users { get; set; }

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

        modelBuilder.Entity<Models.User>()
            .HasQueryFilter(x => !x.IsDeleted);

        UserDbContextSeeds.Seed(modelBuilder);
    }
}