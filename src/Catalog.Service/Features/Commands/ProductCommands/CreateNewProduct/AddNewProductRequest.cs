namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; } = 0;
    public int CategoryId { get; set; }
}