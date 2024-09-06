using System.ComponentModel.DataAnnotations;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductRequest
{
    [Required]
    public int ProductId { get; set; }
    
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? ImageUrl { get; set; } = null;
    public decimal? Price { get; set; } = null;
    public int? Stock { get; set; } = null;

    public int? CategoryId { get; set; } = null;
}