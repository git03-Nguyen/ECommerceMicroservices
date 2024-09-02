using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryCommand : IRequest<AddNewCategoryResponse>
{
    public AddNewCategoryCommand(AddNewCategoryRequest payload)
    {
        Payload = payload;
    }

    public AddNewCategoryRequest Payload { get; }
}