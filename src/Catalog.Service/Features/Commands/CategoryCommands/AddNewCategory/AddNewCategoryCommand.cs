using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryCommand : IRequest<AddNewCategoryResponse>
{
    public AddNewCategoryRequest Payload { get; }
    
    public AddNewCategoryCommand(AddNewCategoryRequest payload)
    {
        Payload = payload;
    }
}