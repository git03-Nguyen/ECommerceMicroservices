using Microsoft.EntityFrameworkCore;
using Product.Service.Models;

namespace Product.Service.Data;

public static class ProductItemContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductItem>().HasData(
            new ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description for Product 1",
                Price = 100
            },
            new ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description for Product 2",
                Price = 200
            },
            new ProductItem
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description for Product 3",
                Price = 300
            }
        );
    }
}