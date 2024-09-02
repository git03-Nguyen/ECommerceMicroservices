using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<bool>
{
    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; set; }
}