using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Repositories;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Queues;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly ICatalogUnitOfWork _unitOfWork;
    private readonly ISendEndpointCustomProvider _sendEndpoint;


    public UpdateProductHandler(ILogger<UpdateProductHandler> logger, ICatalogUnitOfWork unitOfWork, ISendEndpointCustomProvider sendEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _sendEndpoint = sendEndpoint;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Check if product exists
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Request.ProductId);
        
        if (product == null)
        {
            _logger.LogError("Product with id {ProductId} not found.", request.Request.ProductId);
            throw new ProductNotFoundException(request.Request.ProductId);
        }
        
        // Trim whitespaces
        request.Request.Name = request.Request.Name?.Trim();
        request.Request.Description = request.Request.Description?.Trim();
        
        // Update product
        bool priceOrStockedChanged = false;
        product.Name = request.Request.Name ?? product.Name;
        product.Description = request.Request.Description ?? product.Description;
        product.ImageUrl = request.Request.ImageUrl ?? product.ImageUrl;
        if (request.Request.Price != null)
        {
            product.Price = request.Request.Price.Value;
            priceOrStockedChanged = true;
        }
        if (request.Request.Stock != null)
        {
            product.Stock = request.Request.Stock.Value;
            priceOrStockedChanged = true;
        }
        product.CategoryId = request.Request.CategoryId ?? product.CategoryId;
        
        // Save changes
        product.UpdatedDate = DateTime.Now;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Send message event: ProductInfoUpdated to Basket.Service
        // TODO: CHeck if the entity is at MODIFIED state
        var message = new ProductInfoUpdated(product.ProductId, product.Name, product.Description, product.ImageUrl,
            product.CategoryId);
        await _sendEndpoint.SendMessage<ProductInfoUpdated>(message, cancellationToken);
        
        return new UpdateProductResponse(product);
    }
}