using Basket.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.DeleteBasket;

public class DeleteBasketHandler : IRequestHandler<DeleteBasketCommand>
{
    private readonly ILogger<DeleteBasketHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBasketHandler(ILogger<DeleteBasketHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var baskets = _unitOfWork.BasketRepository.GetByCondition(b => b.AccountId == request.Payload.AccountId);
        if (baskets == null) throw new ResourceNotFoundException(nameof(Basket), request.Payload.AccountId.ToString());

        _unitOfWork.BasketRepository.RemoveRange(baskets);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Basket with account id {0} deleted", request.Payload.AccountId);
    }
}