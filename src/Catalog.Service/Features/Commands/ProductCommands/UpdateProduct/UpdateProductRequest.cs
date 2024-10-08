using System.ComponentModel.DataAnnotations;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductRequest
{
    [Required] public int ProductId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; } = null;
    public IFormFile? ImageUpload { get; set; } = null;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public int CategoryId { get; set; }
}