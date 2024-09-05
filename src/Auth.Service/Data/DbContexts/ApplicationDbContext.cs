using Auth.Service.Data.Models;
using Auth.Service.Options;
using Contracts.Middlewares;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auth.Service.Data.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly IOptions<AuthDbOptions> _dbOptions;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<AuthDbOptions> dbOptions) :
        base(options)
    {
        _dbOptions = dbOptions;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseNpgsql(_dbOptions.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>()
            .HasQueryFilter(u => !u.IsDeleted);
        
        ApplicationDbContextSeeds.Seed(builder);
    }
}