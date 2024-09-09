using Basket.Service.Repositories;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductHandler :  IRequestHandler<DeleteProductCommand>
{
    private ILogger<DeleteProductHandler> _logger;
    private IUnitOfWork _unitOfWork;

    public DeleteProductHandler(ILogger<DeleteProductHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var basketItems = _unitOfWork.BasketItemRepository.GetByCondition(bi => bi.ProductId == request.Payload.ProductId);
        _unitOfWork.BasketItemRepository.RemoveRange(basketItems);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product deleted from basket items. ProductId: {ProductId}", request.Payload.ProductId);
    }
}