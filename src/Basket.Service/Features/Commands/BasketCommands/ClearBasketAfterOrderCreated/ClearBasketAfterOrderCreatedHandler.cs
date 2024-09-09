using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.ClearBasketAfterOrderCreated;

public class ClearBasketAfterOrderCreatedHandler : IRequestHandler<ClearBasketAfterOrderCreatedCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<ClearBasketAfterOrderCreatedHandler> _logger;

    public ClearBasketAfterOrderCreatedHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ClearBasketAfterOrderCreatedCommand request, CancellationToken cancellationToken)
    {
        if (request.Payload.BasketId != null)
        {
            // Check if basket exists
            var basket = await _unitOfWork.BasketRepository.GetByIdAsync(request.Payload.BasketId);
            if (basket == null) throw new ResourceNotFoundException("BasketId", request.Payload.BasketId.ToString());

            // Clear basket
            basket.BasketItems.Clear();
            _unitOfWork.BasketRepository.Update(basket);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Basket cleared after order created. BasketId: {BasketId}", request.Payload.BasketId);
    }
}