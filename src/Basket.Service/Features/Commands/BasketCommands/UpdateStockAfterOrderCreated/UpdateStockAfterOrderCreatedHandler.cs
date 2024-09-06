using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedHandler : IRequestHandler<UpdateStockAfterOrderCreatedCommand>
{
    private readonly ILogger<UpdateStockAfterOrderCreatedHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStockAfterOrderCreatedHandler(ILogger<UpdateStockAfterOrderCreatedHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateStockAfterOrderCreatedCommand request, CancellationToken cancellationToken)
    {
        // Update the product stock in the basket
        var productIds = request.Payload.OrderItems.Select(x => x.ProductId).ToList();
        // Get all basket items that have the product id in the order
        var basketItems = await _unitOfWork.BasketItemRepository.GetByCondition(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);
        foreach (var basketItem in basketItems)
        {
            // Update stock
            var orderItem = request.Payload.OrderItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
            if (orderItem != null)
            {
                basketItem.Stock -= orderItem.Quantity;
                if (basketItem.Stock < 0) basketItem.Stock = 0;
                _unitOfWork.BasketItemRepository.Update(basketItem);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product stock updated in basket items. ProductIds: {ProductIds}",
            string.Join(",", productIds));
    }
}