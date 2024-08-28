using Catalog.Service.Data.Models;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryResponse 
{
    public int CategoryId { get; set; }
    
    public AddNewCategoryResponse(Category category)
    {
        CategoryId = category.CategoryId;
    }
    
}