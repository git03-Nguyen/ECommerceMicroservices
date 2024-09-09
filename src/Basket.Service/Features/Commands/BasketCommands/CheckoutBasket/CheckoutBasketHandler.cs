using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Models.Dtos;
using Basket.Service.Repositories;
using Basket.Service.Services;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CatalogService _catalogService;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IIdentityService identityService,
        IUnitOfWork unitOfWork, ISendEndpointCustomProvider sendEndpointCustomProvider, CatalogService catalogService)
    {
        _logger = logger;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
        _catalogService = catalogService;
    }

    public async Task Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.BasketId == request.Payload.BasketId)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException(nameof(Data.Models.Basket), request.Payload.BasketId.ToString());

        // Check if owner of the basket is the same as the user
        _identityService.EnsureIsResourceOwner(basket.AccountId);

        // Check if basket is empty
        if (basket.BasketItems.Count == 0) throw new BasketEmptyException(request.Payload.BasketId);

        // Update all products' price and stock in the basket by syncing with the Catalog service
        await UpdateBasketItemsPriceAndStock(basket.BasketItems);

        // Update the basket
        _unitOfWork.BasketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Check if any products are the basket is out of stock (quantity > stock)
        var outOfStockItems = basket.BasketItems.Where(x => x.Quantity > x.Stock).ToList();
        if (outOfStockItems.Count > 0)
            foreach (var outOfStockItem in outOfStockItems)
            {
                _logger.LogWarning(
                    "Product is out of stock. ProductId: {ProductId}, Quantity: {Quantity}, Stock: {Stock}",
                    outOfStockItem.ProductId, outOfStockItem.Quantity, outOfStockItem.Stock);
                throw new ProductOutOfStockException(outOfStockItem.ProductId);
            }

        // Send checkout command to the order service to create an order
        await SendCheckoutBasketCommand(basket, request.Payload, cancellationToken);

    }
    
    private async Task UpdateBasketItemsPriceAndStock(IEnumerable<Data.Models.BasketItem> basketItems)
    {
        var productIds = basketItems.Select(x => x.ProductId).ToList();
        var response = await _catalogService.GetProductsByIds(productIds);
        if (response == null) throw new ResourceNotFoundException(nameof(ProductDto), "ProductIds");
        var products = response.Payload;
        foreach (var basketItem in basketItems)
        {
            var product = products.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
            if (product == null) throw new ResourceNotFoundException(nameof(ProductDto), basketItem.ProductId.ToString());
            if (basketItem.UnitPrice != product.Price) basketItem.UnitPrice = product.Price;
            if (basketItem.Stock != product.Stock) basketItem.Stock = product.Stock;
        }
    }
    
    private async Task SendCheckoutBasketCommand(Data.Models.Basket basket, CheckoutBasketRequest request, CancellationToken cancellationToken)
    {
        var message = new Contracts.MassTransit.Messages.Commands.CheckoutBasket
        {
            BasketId = basket.BasketId,
            AccountId = basket.AccountId,
            RecipientName = request.RecipientName,
            ShippingAddress = request.ShippingAddress,
            RecipientPhone = request.RecipientPhone,
            BasketItems = basket.BasketItems.Select(x => new CheckoutBasketItem
            {
                BasketItemId = x.BasketItemId,
                BasketId = x.BasketId,
                SellerAccountId = Guid.NewGuid(),
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ImageUrl = x.ImageUrl,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            }).ToList(),
            TotalPrice = basket.BasketItems.Sum(x => x.UnitPrice * x.Quantity)
        };
        await _sendEndpointCustomProvider
            .SendMessage<Contracts.MassTransit.Messages.Commands.CheckoutBasket>(message, cancellationToken);
        _logger.LogInformation("Basket checked out. BasketId: {BasketId}. Waiting to create order.",
            request.BasketId);
    }
}
