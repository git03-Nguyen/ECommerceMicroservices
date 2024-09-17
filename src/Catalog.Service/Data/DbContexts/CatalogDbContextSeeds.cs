using Catalog.Service.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.DbContexts;

public static class CatalogDbContextSeeds
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Product>().

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Quần áo",
                Description = "Đây là danh mục quần áo",
                CreatedDate = new DateTime(2024, 09, 16, 10, 55, 02, 642122),
                UpdatedDate = new DateTime(2024, 09, 16, 10, 55, 02, 642167),
                IsDeleted = false,
                DeletedAt = null
            },
            new Category
            {
                CategoryId = 2,
                Name = "Điện tử",
                Description = "Đây là danh mục điện tử",
                CreatedDate = new DateTime(2024, 09, 16, 10, 55, 13, 704397),
                UpdatedDate = new DateTime(2024, 09, 16, 10, 55, 13, 704400),
                IsDeleted = false,
                DeletedAt = null
            },
            new Category
            {
                CategoryId = 3,
                Name = "Xe cộ",
                Description = "Đây là danh mục xe cộ",
                CreatedDate = new DateTime(2024, 09, 16, 10, 55, 35, 897973),
                UpdatedDate = new DateTime(2024, 09, 16, 10, 55, 35, 897975),
                IsDeleted = false,
                DeletedAt = null
            },
            new Category
            {
                CategoryId = 4,
                Name = "Gia dụng",
                Description = "Đây là danh mục gia dụng",
                CreatedDate = new DateTime(2024, 09, 16, 10, 56, 00, 929039),
                UpdatedDate = new DateTime(2024, 09, 16, 03, 56, 10, 982489),
                IsDeleted = false,
                DeletedAt = null
            }
        );
    }
}