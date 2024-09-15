using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<DeleteProductHandler> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(ILogger<DeleteProductHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService, ISendEndpointCustomProvider sendEndpointCustomProvider)
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
        if (product == null) throw new ResourceNotFoundException(nameof(Product), request.Id.ToString());

        // Check if the user is the owner of the product or an admin
        _identityService.EnsureIsAdminOrOwner(product.SellerId);

        // Check if the server contains the image or the image is from an external source
        if (product.IsOwnImage)
        {
            // Delete the image
            var filePath = Path.Combine("wwwroot", product.ImageUrl);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        // Delete the product
        _unitOfWork.ProductRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish message: ProductDeleted
        await SendDeleteProductCommand(product, cancellationToken);

        _logger.LogInformation("Product deleted. Id: {Id}", request.Id);
    }

    private async Task SendDeleteProductCommand(Product product, CancellationToken cancellationToken)
    {
        var message = new
        {
            ProductIds = new[]
            {
                product.ProductId
            }
        };
        await _sendEndpointCustomProvider.SendMessage<IDeleteProducts>(message, cancellationToken);
    }
}