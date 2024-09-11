using System.Text.Json.Serialization;
using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryResponse
{
    public CategoryDto Payload { get; }

    public UpdateCategoryResponse(Category category)
    {
        Payload = new CategoryDto(category);
    }
}