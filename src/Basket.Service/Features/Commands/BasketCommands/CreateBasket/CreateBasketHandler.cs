using Basket.Service.Data.Models;
using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, CreateBasketResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
     

    public CreateBasketHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<CreateBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var identity = _identityService.GetUserInfoIdentity();
        var userId = _identityService.GetUserId();
        
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // Make new basket
            var newBasket = new Data.Models.Basket
            {
                BuyerId = userId,
            };
            await _unitOfWork.BasketRepository.AddAsync(newBasket);
            
            // Get the product from Catalog.Service
            
            
            // create new basket item
            var newBasketItem = new BasketItem
            {
                BasketId = newBasket.BasketId,
                ProductId = request.Payload.ProductId,
                Quantity = request.Payload.ProductQuantity,
                
                // Maybe wrong, TODO: check this from Catalog.Service by Message Queue
                ProductName = request.Payload.InitProductName,
                ImageUrl = request.Payload.InitProductImageUrl,
                UnitPrice = request.Payload.InitProductUnitPrice
            };
            await _unitOfWork.BasketItemRepository.AddAsync(newBasketItem);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            
            newBasket.BasketItems.Add(newBasketItem);
            return new CreateBasketResponse(newBasket);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}