using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public UpdateProductCommand(UpdateProductRequest request)
    {
        Request = request;
    }

    public UpdateProductRequest Request { get; }
}