using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public UpdateCategoryRequest Payload { get; }
    
    public UpdateCategoryCommand(UpdateCategoryRequest payload)
    {
        Payload = payload;
    }
}