using System.ComponentModel.DataAnnotations;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryRequest
{
    [Required] public int CategoryId { get; set; }

    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? ImageUrl { get; set; } = null;
}