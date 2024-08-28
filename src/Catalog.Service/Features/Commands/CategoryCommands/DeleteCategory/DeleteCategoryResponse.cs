using Catalog.Service.Data.Models;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryResponse
{
    public int CategoryId { get; set; }
    
    public DeleteCategoryResponse(int categoryId)
    {
        CategoryId = categoryId;
    }
    
}