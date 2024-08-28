using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
    
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}