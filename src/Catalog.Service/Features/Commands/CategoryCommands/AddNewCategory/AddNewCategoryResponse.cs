using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryResponse
{
    public AddNewCategoryResponse(Category category)
    {
        Payload = new CategoryDto(category);
    }

    public CategoryDto Payload { get; set; }
}