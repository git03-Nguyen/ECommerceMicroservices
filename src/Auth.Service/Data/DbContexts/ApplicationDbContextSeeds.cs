using Auth.Service.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Data.DbContexts;

public static class ApplicationDbContextSeeds
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
                { Id = Guid.Parse("c32ba259-6094-474b-a730-60b8aae724e2"), Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole
            {
                Id = Guid.Parse("d999706f-5829-4be8-bc51-05383533dfb3"), Name = "Customer", NormalizedName = "CUSTOMER"
            }
        );
    }
}