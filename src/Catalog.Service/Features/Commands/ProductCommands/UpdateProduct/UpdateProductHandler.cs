using Catalog.Service.Exceptions;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Repositories;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Queues;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpoint;
    private readonly ICatalogUnitOfWork _unitOfWork;


    public UpdateProductHandler(ILogger<UpdateProductHandler> logger, ICatalogUnitOfWork unitOfWork,
        ISendEndpointCustomProvider sendEndpoint)
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
            throw new ResourceNotFoundException("Product", request.Request.ProductId.ToString());
        }

        // Trim whitespaces
        request.Request.Name = request.Request.Name?.Trim();
        request.Request.Description = request.Request.Description?.Trim();

        // Update product
        product.Name = request.Request.Name ?? product.Name;
        product.Description = request.Request.Description ?? product.Description;
        product.ImageUrl = request.Request.ImageUrl ?? product.ImageUrl;
        product.CategoryId = request.Request.CategoryId ?? product.CategoryId;
        product.Price = request.Request.Price ?? product.Price;
        product.Stock = request.Request.Stock ?? product.Stock;

        var priceOrStockedChanged = product.Price != request.Request.Price || product.Stock != request.Request.Stock;
        var productInfoChanged = product.Name != request.Request.Name ||
                                 product.Description != request.Request.Description ||
                                 product.ImageUrl != request.Request.ImageUrl ||
                                 product.CategoryId != request.Request.CategoryId;

        // Save changes
        product.UpdatedDate = DateTime.Now;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send message event: ProductInfoUpdated to Basket.Service
        if (productInfoChanged)
        {
            var message = new ProductInfoUpdated(product.ProductId, product.Name, product.Description, product.ImageUrl,
                product.CategoryId);
            await _sendEndpoint.SendMessage<ProductInfoUpdated>(message, cancellationToken);
        }

        // Send message event: ProductInfoUpdated to Basket.Service
        if (priceOrStockedChanged)
        {
            var message = new ProductPriceStockUpdated(product.ProductId, product.Price, product.Stock);
            await _sendEndpoint.SendMessage<ProductPriceStockUpdated>(message, cancellationToken);
        }

        return new UpdateProductResponse(product);
    }
}