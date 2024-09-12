namespace Catalog.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public IFormFile? ImageUpload { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;
    public int CategoryId { get; set; }
}