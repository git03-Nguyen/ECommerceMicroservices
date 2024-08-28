using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; set; }
    
    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}