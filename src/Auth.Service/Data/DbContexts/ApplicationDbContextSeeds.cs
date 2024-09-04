using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Data.DbContexts;

public static class ApplicationDbContextSeeds
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = Guid.Parse("c32ba259-6094-474b-a730-60b8aae724e2"), Name = "Admin", NormalizedName = "ADMIN"
            },
            new ApplicationRole
            {
                Id = Guid.Parse("d999706f-5829-4be8-bc51-05383533dfb3"), Name = "Customer", NormalizedName = "CUSTOMER"
            },
            new ApplicationRole
            {
                Id = Guid.Parse("eb161112-0780-4099-94cc-c89a78257aff"), Name = "Seller", NormalizedName = "SELLER"
            }
        );
        
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                // password: Admin@123
                Id = Guid.Parse("9de65cd0-9b44-4266-a902-d8d907a13671"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFbM0iIX4wZv1ay/yZApBfh5f6Rv60QDiMxUAvvu+lUfdj3SNhAJpoI+jcvg+v9DbQ==",
                SecurityStamp = "TQXRJCFWDCRPAM7NWGC6DL2G3W5MMXKT",
                ConcurrencyStamp = "b5c97c3c-4201-452b-a3c8-e3a74cc1e1f9"
            },
            new ApplicationUser
            {
                // password: Customer@123
                Id = Guid.Parse("961e9858-1a50-4879-b387-1c2482148515"),
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@customer.com",
                PasswordHash = "AQAAAAIAAYagAAAAEKYgUl4MwIt17ye0TpTrB37oyo8f+xhS7PyqndfRgjDw7d5kSuLzvuCFb4RtyT5e2A==",
                SecurityStamp = "WYCJPDDE7OWUJLMPDNTJYIRCK2IGWOJN",
                ConcurrencyStamp = "77bfdf97-1d0e-42a1-af9b-92e58e5871c3"
            },
            new ApplicationUser
            {
                // password: Seller@123
                Id = Guid.Parse("470fa3b5-1b0e-4d11-b992-8b2ada4825b8"),
                UserName = "seller",
                NormalizedUserName = "SELLER",
                Email = "seller@seller.com",
                PasswordHash = "AQAAAAIAAYagAAAAEDJkSPcr2H8KP7AOrUyVs+vVund5GaF8wvJS/AlnOgmTOT/IIjaTBjdlPZZypeRegA==",
                SecurityStamp = "UYHS6CDSNIGDOYH5HDOTS4A2YWMSU7CO",
                ConcurrencyStamp = "6e995744-d06e-499c-beff-b494ee11ca3c"
            }
        );   
        
        // Add roles to users
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("c32ba259-6094-474b-a730-60b8aae724e2"),
                UserId = Guid.Parse("9de65cd0-9b44-4266-a902-d8d907a13671")
            },
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("d999706f-5829-4be8-bc51-05383533dfb3"),
                UserId = Guid.Parse("961e9858-1a50-4879-b387-1c2482148515")
            },
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("eb161112-0780-4099-94cc-c89a78257aff"),
                UserId = Guid.Parse("470fa3b5-1b0e-4d11-b992-8b2ada4825b8")
            }
        );
    }
}