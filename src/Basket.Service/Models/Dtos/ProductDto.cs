namespace Basket.Service.Models.Dtos;

public class ProductDto
{
    public int Id { get; set; }
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