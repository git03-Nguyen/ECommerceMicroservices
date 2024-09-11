using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerHandler : IRequestHandler<DeleteSellerCommand>
{
    private readonly ILogger<DeleteSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISendEndpointCustomProvider _sendEndpoint;

    public DeleteSellerHandler(ILogger<DeleteSellerHandler> logger, IUnitOfWork unitOfWork, ISendEndpointCustomProvider sendEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _sendEndpoint = sendEndpoint;
    }

    public async Task Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.ProductRepository.GetByCondition(x => x.SellerAccountId == request.Payload.AccountId);
        _unitOfWork.ProductRepository.RemoveRange(products);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Products of seller with id {SellerId} deleted", request.Payload.AccountId);
        
        // Send message: ProductDeleted to Basket.Service
        await SendProductDeletedCommand(products, cancellationToken);
    }
    
    private async Task SendProductDeletedCommand(IEnumerable<Product> products, CancellationToken cancellationToken)
    {
        var message = new DeleteProducts
        {
            ProductIds = products.Select(x => x.ProductId).ToArray()
        };
        await _sendEndpoint.SendMessage<DeleteProducts>(message, cancellationToken);
    }
}