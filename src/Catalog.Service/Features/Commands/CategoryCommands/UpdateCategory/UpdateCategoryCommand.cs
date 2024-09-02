using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public UpdateCategoryCommand(UpdateCategoryRequest payload)
    {
        Payload = payload;
    }

    public UpdateCategoryRequest Payload { get; }
}