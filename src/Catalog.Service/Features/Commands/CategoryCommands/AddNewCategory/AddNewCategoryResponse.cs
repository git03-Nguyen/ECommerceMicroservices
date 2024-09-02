using Catalog.Service.Data.Models;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryResponse
{
    public AddNewCategoryResponse(Category category)
    {
        CategoryId = category.CategoryId;
    }

    public int CategoryId { get; set; }
}