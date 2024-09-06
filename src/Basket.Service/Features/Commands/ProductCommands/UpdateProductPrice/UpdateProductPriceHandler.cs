using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductPrice;

public class UpdateProductPriceHandler : IRequestHandler<UpdateProductPriceCommand>
{
    private readonly ILogger<UpdateProductPriceHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceHandler(ILogger<UpdateProductPriceHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var basketItems =
            _unitOfWork.BasketItemRepository.GetByCondition(bi => bi.ProductId == request.Payload.ProductId);

        foreach (var basketItem in basketItems)
            if (basketItem.UnitPrice != request.Payload.Price)
                basketItem.UnitPrice = request.Payload.Price;

        _unitOfWork.BasketItemRepository.UpdateRange(await basketItems.ToListAsync(cancellationToken));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product price updated in basket items. ProductId: {ProductId}, Price: {Price}",
            request.Payload.ProductId, request.Payload.Price);
    }
}