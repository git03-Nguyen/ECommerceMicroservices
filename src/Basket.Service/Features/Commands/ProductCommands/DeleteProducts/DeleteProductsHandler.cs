using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProducts;

public class DeleteProductsHandler :  IRequestHandler<DeleteProductsCommand>
{
    private ILogger<DeleteProductsHandler> _logger;
    private IUnitOfWork _unitOfWork;

    public DeleteProductsHandler(ILogger<DeleteProductsHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Payload.ProductIds;
        try
        {
            var products = await _unitOfWork.ProductRepository.GetByCondition(x => productIds.Contains(x.ProductId)).ToListAsync(cancellationToken);
            if (products.Count != productIds.Count())
            {
                var missingProductIds = productIds.Except(products.Select(x => x.ProductId));
                _logger.LogWarning("Some products are missing. ProductIds: {ProductIds}", string.Join(", ", missingProductIds));
            }
            _unitOfWork.ProductRepository.RemoveRange(products);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Products deleted from basket items. ProductIds: {ProductIds}", string.Join(", ", request.Payload.ProductIds));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting products. ProductIds: {ProductIds}", string.Join(", ", request.Payload.ProductIds));
            throw;
        }
    }
}