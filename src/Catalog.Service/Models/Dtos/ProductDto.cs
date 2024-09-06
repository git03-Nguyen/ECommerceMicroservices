using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Dtos;

public class ProductDto
{
    // Limit the properties to be returned
    public ProductDto(Product product)
    {
        ProductId = product.ProductId;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        ImageUrl = product.ImageUrl;
        Stock = product.Stock;
        CreatedDate = product.CreatedDate;
        UpdatedDate = product.UpdatedDate;
        CategoryId = product.CategoryId;
        CategoryName = product.Category.Name;
        SellerAccountId = product.SellerAccountId;
        SellerName = product.SellerName;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int Stock { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public Guid SellerAccountId { get; set; }
    public string SellerName { get; set; }
}