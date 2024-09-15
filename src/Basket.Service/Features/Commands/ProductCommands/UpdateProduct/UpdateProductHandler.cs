using Basket.Service.Repositories;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IUnitOfWork unitOfWork, ILogger<UpdateProductHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Payload.ProductId);
        if (product == null)
        {
            _logger.LogWarning("Product not found for product id: {Id}", request.Payload.ProductId);
            return;
        }

        if (product.ProductName != request.Payload.Name) product.ProductName = request.Payload.Name;

        if (product.ImageUrl != request.Payload.ImageUrl) product.ImageUrl = request.Payload.ImageUrl;

        if (product.UnitPrice != request.Payload.Price) product.UnitPrice = request.Payload.Price;
        if (product.Stock != request.Payload.Stock) product.Stock = request.Payload.Stock;

        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product info updated for product id: {Id}", request.Payload.ProductId);
    }
}