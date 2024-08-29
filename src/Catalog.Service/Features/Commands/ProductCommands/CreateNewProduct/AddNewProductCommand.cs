using Catalog.Service.Data.Models;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductCommand : IRequest<AddNewProductResponse>
{
    public AddNewProductRequest Payload { get; set; }
    
    public AddNewProductCommand(AddNewProductRequest payload)
    {
        Payload = payload;
    }
    
}