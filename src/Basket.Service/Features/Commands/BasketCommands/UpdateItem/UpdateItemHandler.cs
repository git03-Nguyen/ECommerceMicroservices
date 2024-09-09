using Basket.Service.Data.Models;
using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<UpdateItemHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateItemHandler(ILogger<UpdateItemHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<UpdateItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.BasketId == request.Payload.BasketId)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException("BasketId", request.Payload.BasketId.ToString());

        // Check if owner of the basket is the same as the user
        var isOwner = _identityService.IsResourceOwner(basket.AccountId);
        if (!isOwner) throw new UnAuthorizedAccessException();

        // TODO: Check if product exists and the data is true

        // Check if product is already in the basket
        var oldItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.Payload.ProductId);
        if (oldItem != null)
        {
            // If quantity is 0, remove product from basket
            if (request.Payload.Quantity == 0)
            {
                basket.BasketItems.Remove(oldItem);
            }
            else
            {
                // Else, update quantity (but >= 0)
                oldItem.Quantity += request.Payload.Quantity;
                if (oldItem.Quantity < 0) oldItem.Quantity = 0;
            }
        }
        else
        {
            // If no same product, add product to basket
            var newItem = new BasketItem
            {
                BasketId = request.Payload.BasketId,
                SellerAccountId = request.Payload.SellerAccountId,
                ProductId = request.Payload.ProductId,
                Quantity = request.Payload.Quantity,
                ProductName = request.Payload.ProductName,
                UnitPrice = request.Payload.UnitPrice,
                ImageUrl = request.Payload.ImageUrl
            };
            basket.BasketItems.Add(newItem);
        }

        _unitOfWork.BasketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateItemResponse(basket);
    }
}