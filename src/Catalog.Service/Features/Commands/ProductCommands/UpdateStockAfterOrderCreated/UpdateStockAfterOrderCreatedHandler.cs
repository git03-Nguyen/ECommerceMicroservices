using Catalog.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedHandler : IRequestHandler<UpdateStockAfterOrderCreatedCommand>
{
    private readonly ILogger<UpdateStockAfterOrderCreatedHandler> _logger;
    private readonly ICatalogUnitOfWork _unitOfWork;

    public UpdateStockAfterOrderCreatedHandler(ILogger<UpdateStockAfterOrderCreatedHandler> logger,
        ICatalogUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateStockAfterOrderCreatedCommand request, CancellationToken cancellationToken)
    {
        // Get all products in the order
        var productIds = request.Payload.OrderItems.Select(x => x.ProductId).ToList();
        var products = await _unitOfWork.ProductRepository.GetByCondition(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);

        // Update stock
        foreach (var product in products)
        {
            var orderItem = request.Payload.OrderItems.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (orderItem != null)
            {
                product.Stock -= orderItem.Quantity;
                if (product.Stock < 0) product.Stock = 0;
                _unitOfWork.ProductRepository.Update(product);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product stock updated after order created. ProductIds: {ProductIds}",
            string.Join(",", productIds));
    }
}