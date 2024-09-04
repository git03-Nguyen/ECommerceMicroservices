using Catalog.Service.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.DbContexts;

public static class CatalogDbContextSeeds
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1, Name = "Áo thun nam", Description = "Áo thun nam hàng hiệu màu đỏ", Price = 15,
                CategoryId = 1, Stock = 100
            },
            new Product
            {
                ProductId = 2, Name = "Áo thun nữ", Description = "Áo thun nữ hàng hiệu màu hồng", Price = 20,
                CategoryId = 1, Stock = 100
            },
            new Product
            {
                ProductId = 3, Name = "Quần jean nam", Description = "Quần jean nam hàng hiệu màu xanh", Price = 30,
                CategoryId = 2, Stock = 100
            },
            new Product
            {
                ProductId = 4, Name = "Quần jean nữ", Description = "Quần jean nữ hàng hiệu màu xanh", Price = 25,
                CategoryId = 2, Stock = 100
            },
            new Product
            {
                ProductId = 5, Name = "Giày thể thao nam", Description = "Giày thể thao nam hàng hiệu màu trắng",
                Price = 50, CategoryId = 3, Stock = 100
            },
            new Product
            {
                ProductId = 6, Name = "Giày thể thao nữ", Description = "Giày thể thao nữ hàng hiệu màu trắng",
                Price = 45, CategoryId = 3, Stock = 100
            },
            new Product
            {
                ProductId = 7, Name = "Đồng hồ nam", Description = "Đồng hồ nam hàng hiệu màu đen", Price = 100,
                CategoryId = 4, Stock = 100
            },
            new Product
            {
                ProductId = 8, Name = "Đồng hồ nữ", Description = "Đồng hồ nữ hàng hiệu màu đen", Price = 90,
                CategoryId = 4, Stock = 100
            },
            new Product
            {
                ProductId = 9, Name = "Túi xách nam", Description = "Túi xách nam hàng hiệu màu nâu", Price = 70,
                CategoryId = 5, Stock = 100
            },
            new Product
            {
                ProductId = 10, Name = "Túi xách nữ", Description = "Túi xách nữ hàng hiệu màu nâu", Price = 60,
                CategoryId = 5, Stock = 100
            },
            new Product
            {
                ProductId = 11, Name = "Mũ nam", Description = "Mũ nam hàng hiệu màu xanh", Price = 10, CategoryId = 6,
                Stock = 100
            },
            new Product
            {
                ProductId = 12, Name = "Mũ nữ", Description = "Mũ nữ hàng hiệu màu xanh", Price = 10, CategoryId = 6,
                Stock = 100
            },
            new Product
            {
                ProductId = 13, Name = "Kính râm nam", Description = "Kính râm nam hàng hiệu màu đen", Price = 20,
                CategoryId = 7, Stock = 100
            },
            new Product
            {
                ProductId = 14, Name = "Kính râm nữ", Description = "Kính râm nữ hàng hiệu màu đen", Price = 20,
                CategoryId = 7, Stock = 100
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Áo thun" },
            new Category { CategoryId = 2, Name = "Quần jean" },
            new Category { CategoryId = 3, Name = "Giày thể thao" },
            new Category { CategoryId = 4, Name = "Đồng hồ" },
            new Category { CategoryId = 5, Name = "Túi xách" },
            new Category { CategoryId = 6, Name = "Mũ" },
            new Category { CategoryId = 7, Name = "Kính râm" }
        );
    }
}