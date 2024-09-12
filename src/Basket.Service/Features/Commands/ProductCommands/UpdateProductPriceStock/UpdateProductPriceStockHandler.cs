using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductPriceStock;

public class UpdateProductPriceStockHandler : IRequestHandler<UpdateProductPriceStockCommand>
{
    private readonly ILogger<UpdateProductPriceStockHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceStockHandler(ILogger<UpdateProductPriceStockHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductPriceStockCommand request, CancellationToken cancellationToken)
    {
        var basketItems =
            _unitOfWork.BasketItemRepository.GetByCondition(bi => bi.ProductId == request.Payload.ProductId);

        foreach (var basketItem in basketItems)
        {
            if (basketItem.UnitPrice != request.Payload.Price)
                basketItem.UnitPrice = request.Payload.Price;
            if (basketItem.Stock != request.Payload.Stock)
                basketItem.Stock = request.Payload.Stock;
        }

        _unitOfWork.BasketItemRepository.UpdateRange(await basketItems.ToListAsync(cancellationToken));
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product price updated in basket items. Id: {Id}, Price: {Price}",
            request.Payload.ProductId, request.Payload.Price);
        _logger.LogInformation("Product stock updated in basket items. Id: {Id}, Stock: {Stock}",
            request.Payload.ProductId, request.Payload.Stock);
    }
}