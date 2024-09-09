using Basket.Service.Data.Models;
using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Models.Dtos;
using Basket.Service.Repositories;
using Basket.Service.Services;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<UpdateItemHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CatalogService _catalogService;

    public UpdateItemHandler(ILogger<UpdateItemHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService, CatalogService catalogService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _catalogService = catalogService;
    }

    public async Task<UpdateItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByIdAsync(request.Payload.BasketId);
        if (basket == null) throw new ResourceNotFoundException(nameof(Data.Models.Basket), request.Payload.BasketId.ToString());

        // Check if owner of the basket is the same as the user
        _identityService.EnsureIsResourceOwner(basket.AccountId);
        
        // TODO: Check if product exists 
        var response = await _catalogService.GetProductById(request.Payload.ProductId);
        var product = response?.Payload;
        if (product == null) throw new ResourceNotFoundException(nameof(ProductDto), request.Payload.ProductId.ToString());

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
                // Else, update quantity 
                oldItem.Quantity = request.Payload.Quantity;
                if (oldItem.Quantity > product.Stock)
                {
                    throw new ProductOutOfStockException(request.Payload.ProductId);
                }
            }
        }
        else
        {
            // If no same product, add product to basket
            var newItem = new BasketItem
            {
                BasketId = request.Payload.BasketId,
                SellerAccountId = product.SellerAccountId,
                ProductId = request.Payload.ProductId,
                Quantity = request.Payload.Quantity,
                ProductName = product.Name,
                UnitPrice = product.Price,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock
            };
            if (newItem.Quantity > product.Stock)
            {
                throw new ProductOutOfStockException(request.Payload.ProductId);
            }
            basket.BasketItems.Add(newItem);
        }

        _unitOfWork.BasketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateItemResponse(basket);
    }
}