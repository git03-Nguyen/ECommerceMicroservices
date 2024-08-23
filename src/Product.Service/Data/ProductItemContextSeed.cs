using Microsoft.EntityFrameworkCore;

namespace Product.Service.Data;

public static class ProductItemContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.ProductItem>().HasData(
            new Models.ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description for Product 1",
                Price = 100
            },
            new Models.ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description for Product 2",
                Price = 200
            },
            new Models.ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description for Product 3",
                Price = 300
            }
        );
    }
}