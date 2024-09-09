using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductCommand : IRequest<AddNewProductResponse>
{
    public AddNewProductCommand(AddNewProductRequest payload)
    {
        Payload = payload;
    }

    public AddNewProductRequest Payload { get; set; }
}