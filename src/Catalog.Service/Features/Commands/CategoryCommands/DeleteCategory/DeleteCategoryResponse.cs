namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryResponse
{
    public DeleteCategoryResponse(int categoryId)
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; set; }
}