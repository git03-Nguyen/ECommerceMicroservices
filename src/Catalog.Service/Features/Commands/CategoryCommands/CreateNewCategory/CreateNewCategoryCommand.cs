using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Service.Features.Commands.CategoryCommands.CreateNewCategory;

public class CreateNewCategoryCommand : IRequest<CreateNewCategoryResponse>
{
    public CreateNewCategoryRequest Payload { get; }
    
    public CreateNewCategoryCommand(CreateNewCategoryRequest payload)
    {
        Payload = payload;
    }
}