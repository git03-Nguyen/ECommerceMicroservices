using Microsoft.EntityFrameworkCore;

namespace User.Service.Data.DbContexts;

public static class UserDbContextSeeds
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.User>().HasData(
            new Models.User
            {
                UserId = Guid.Parse("9de65cd0-9b44-4266-a902-d8d907a13671"),
                UserName = "admin",
                FullName = "Quản trị viên",
                Email = "admin@admin.com",
                PhoneNumber = "0123456789",
                Address = "TP Hồ Chí Minh",
                Role = "Admin",
                CreatedAt = DateTimeOffset.Parse("2021-08-01T00:00:00+07:00"),
                UpdatedAt = DateTimeOffset.Parse("2021-08-01T00:00:00+07:00"),
                IsDeleted = false,
                DeletedAt = null
            }
        );
    }
}