namespace Catalog.Service.Data.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public int? CategoryId { get; set; } = null;
    public Category? Category { get; set; } = null;
}