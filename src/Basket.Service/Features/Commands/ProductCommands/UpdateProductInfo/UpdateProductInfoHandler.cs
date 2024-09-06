using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductInfo;

public class UpdateProductInfoHandler : IRequestHandler<UpdateProductInfoCommand>
{
    private readonly ILogger<UpdateProductInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductInfoHandler(IUnitOfWork unitOfWork, ILogger<UpdateProductInfoHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UpdateProductInfoCommand request, CancellationToken cancellationToken)
    {
        var basketItems =
            _unitOfWork.BasketItemRepository.GetByCondition(x => x.ProductId == request.Payload.ProductId);
        foreach (var basketItem in basketItems)
        {
            basketItem.ProductName = request.Payload.Name;
            basketItem.ImageUrl = request.Payload.ImageUrl;
        }

        _unitOfWork.BasketItemRepository.UpdateRange(await basketItems.ToListAsync(cancellationToken));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product info updated for product id: {ProductId}", request.Payload.ProductId);
    }
}