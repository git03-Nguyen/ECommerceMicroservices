using Contracts.MassTransit.Messages.Commands;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductCommand : IRequest
{
    public AddNewProductCommand(ICreateProduct payload)
    {
        Payload = payload;
    }

    public ICreateProduct Payload { get; }
}