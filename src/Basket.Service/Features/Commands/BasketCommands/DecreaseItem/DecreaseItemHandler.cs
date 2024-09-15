using Basket.Service.Data.Models;
using Basket.Service.Exceptions;
using Basket.Service.Features.Commands.BasketCommands.IncreaseItem;
using Basket.Service.Repositories;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.DecreaseItem;

public class DecreaseItemHandler : IRequestHandler<DecreaseItemCommand, UpdateItemResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<DecreaseItemHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DecreaseItemHandler(ILogger<DecreaseItemHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<UpdateItemResponse> Handle(DecreaseItemCommand request, CancellationToken cancellationToken)
    {
        // Check if owner of the basket is the same as the user and is a CUSTOMER
        _identityService.EnsureIsCustomer();
        var userId = _identityService.GetUserId();

        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException(nameof(Data.Models.Basket), userId.ToString());

        // Check if product is already in the basket
        var oldItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.Payload.ProductId);
        if (oldItem == null)
            throw new ResourceNotFoundException(nameof(BasketItem), request.Payload.ProductId.ToString());

        // Decrease quantity
        oldItem.Quantity -= request.Payload.Quantity;
        if (oldItem.Quantity < 0) throw new ProductOutOfStockException(request.Payload.ProductId);
        if (oldItem.Quantity == 0) basket.BasketItems.Remove(oldItem);

        _unitOfWork.BasketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateItemResponse(basket);
    }
}