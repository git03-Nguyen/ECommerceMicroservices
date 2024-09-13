using Basket.Service.Repositories;
using MediatR;

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
        var basketItems = _unitOfWork.BasketItemRepository.GetByCondition(bi => request.Payload.ProductIds.Contains(bi.ProductId));
        _unitOfWork.BasketItemRepository.RemoveRange(basketItems);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Products deleted from basket items. ProductIds: {ProductIds}", string.Join(", ", request.Payload.ProductIds));
    }
}