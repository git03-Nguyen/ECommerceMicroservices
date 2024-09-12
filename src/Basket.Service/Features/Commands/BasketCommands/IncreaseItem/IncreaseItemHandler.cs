using Basket.Service.Data.Models;
using Basket.Service.Exceptions;
using Basket.Service.Models.Dtos;
using Basket.Service.Repositories;
using Basket.Service.Services;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.IncreaseItem;

public class IncreaseItemHandler : IRequestHandler<IncreaseItemCommand, UpdateItemResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<IncreaseItemHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CatalogService _catalogService;

    public IncreaseItemHandler(ILogger<IncreaseItemHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService, CatalogService catalogService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _catalogService = catalogService;
    }

    public async Task<UpdateItemResponse> Handle(IncreaseItemCommand request, CancellationToken cancellationToken)
    {
        // Check if owner of the basket is the same as the user and is a CUSTOMER
        _identityService.EnsureIsCustomer();
        var userId = _identityService.GetUserId();
        
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == userId)
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException(nameof(Data.Models.Basket), userId.ToString());
        
        // Check if product exists 
        var response = await _catalogService.GetProductById(request.Payload.ProductId);
        var product = response?.Payload;
        if (product == null) throw new ResourceNotFoundException(nameof(ProductDto), request.Payload.ProductId.ToString());

        // Check if product is already in the basket
        var oldItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == request.Payload.ProductId);
        if (oldItem != null)
        {
            // Increase quantity 
            oldItem.Quantity += request.Payload.Quantity;
            if (oldItem.Quantity > product.Stock)
            {
                throw new ProductOutOfStockException(request.Payload.ProductId);
            }
        }
        else
        {
            // If no same product, check if product exists and add to basket
            var newItem = new BasketItem
            {
                BasketId = basket.BasketId,
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