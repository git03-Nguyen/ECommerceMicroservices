using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class CreateNewProductCommand : IRequest<AddNewProductResponse>
{
    public AddNewProductRequest Payload { get; set; }
    
    public CreateNewProductCommand(AddNewProductRequest payload)
    {
        Payload = payload;
    }
    
}