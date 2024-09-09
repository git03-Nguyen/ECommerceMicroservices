using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly ILogger<DeleteProductHandler> _logger;
    private readonly ICatalogUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;

    public DeleteProductHandler(ILogger<DeleteProductHandler> logger, ICatalogUnitOfWork unitOfWork, IIdentityService identityService, ISendEndpointCustomProvider sendEndpointCustomProvider)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // Check if product exists
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product == null) throw new ResourceNotFoundException(nameof(Data.Models.Product), request.Id.ToString());

        // Check if the user is the owner of the product or an admin
        _identityService.EnsureIsAdminOrOwner(product.SellerAccountId);

        // Delete the product
        _unitOfWork.ProductRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Publish message: ProductDeleted
        await SendDeleteProductCommand(product, cancellationToken);

        _logger.LogInformation("Product deleted. ProductId: {ProductId}", request.Id);
    }
    
    private async Task SendDeleteProductCommand(Data.Models.Product product, CancellationToken cancellationToken)
    {
        var message = new Contracts.MassTransit.Messages.Commands.DeleteProduct
        {
            ProductId = product.ProductId
        };
        await _sendEndpointCustomProvider.SendMessage<Contracts.MassTransit.Messages.Commands.DeleteProduct>(message, cancellationToken);
    }
    
}