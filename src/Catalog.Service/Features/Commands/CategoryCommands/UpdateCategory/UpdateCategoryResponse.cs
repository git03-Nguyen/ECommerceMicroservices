using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryResponse
{
    public UpdateCategoryResponse(Category category)
    {
        Payload = new CategoryDto(category);
    }

    public CategoryDto Payload { get; }
}