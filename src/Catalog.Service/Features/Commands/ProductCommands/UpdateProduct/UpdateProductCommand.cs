using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public UpdateProductCommand(UpdateProductRequest payload)
    {
        Payload = payload;
    }

    public UpdateProductRequest Payload { get; }
}