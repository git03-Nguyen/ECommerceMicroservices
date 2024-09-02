using System.Text.Json.Serialization;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryResponse
{
    public int CategoryId { get; set; }
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ImageUrl { get; set; }
}