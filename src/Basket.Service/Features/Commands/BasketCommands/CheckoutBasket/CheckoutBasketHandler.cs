using Basket.Service.Exceptions;
using Basket.Service.Repositories;
using Basket.Service.Services;
using Contracts.Exceptions;
using Contracts.MassTransit.Endpoints.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand, CheckoutBasketResponse>
{
    private readonly CatalogService _catalogService;
    private readonly IIdentityService _identityService;
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IIdentityService identityService,
        IUnitOfWork unitOfWork, CatalogService catalogService, ISendEndpointCustomProvider sendEndpointCustomProvider)
    {
        _logger = logger;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _catalogService = catalogService;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
    }

    public async Task<CheckoutBasketResponse> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        // Get the basket of the user
        _identityService.EnsureIsCustomer();
        var userId = _identityService.GetUserId();

        // Check if basket exists, and not empty
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException(nameof(Basket), userId.ToString());
        if (basket.BasketItems.Count == 0) throw new BasketEmptyException(basket.BasketId);

        // Update all products' price and stock in the basket by syncing with the Catalog service => BAD PRACTICE but I think it's safe?
        // TODO: any better ways to do this for concurrency?
        // await UpdateBasketItemsPriceAndStock(basket.BasketItems);
        // _unitOfWork.BasketRepository.Update(basket);
        // await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Check if any products are the basket is out of stock (quantity > stock)
        var outOfStockItems = basket.BasketItems.Where(x => x.Quantity > x.Product.Stock).ToList();
        if (outOfStockItems.Count > 0)
            foreach (var outOfStockItem in outOfStockItems)
            {
                _logger.LogWarning(
                    "Product is out of stock. Id: {Id}, Quantity: {Quantity}, Stock: {Stock}",
                    outOfStockItem.ProductId, outOfStockItem.Quantity, outOfStockItem.Product.Stock);
                throw new ProductOutOfStockException(outOfStockItem.ProductId);
            }

        // Send checkout command (Direct exchange in RMQ) to the order service to create an order
        await SendCheckoutBasketCommand(basket, request.Payload, cancellationToken);

        // Clear the basket
        basket.BasketItems.Clear();
        _unitOfWork.BasketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return the basket
        return new CheckoutBasketResponse(basket);
    }

    private async Task SendCheckoutBasketCommand(Data.Models.Basket basket, CheckoutBasketRequest request,
        CancellationToken cancellationToken)
    {
        var message = new
        {
            basket.BasketId,
            basket.AccountId,
            RecipientName = request.FullName,
            request.ShippingAddress,
            RecipientPhone = request.PhoneNumber,
            BasketItems = basket.BasketItems.Select(x => new
            {
                x.BasketItemId,
                x.BasketId,
                SellerAccountId = x.Product.SellerId,
                SellerAccountName = x.Product.Seller.Name,
                x.ProductId,
                x.Product.ProductName,
                x.Product.ImageUrl,
                x.Product.UnitPrice,
                x.Quantity
            }),

            TotalPrice = basket.BasketItems.Sum(x => x.Product.UnitPrice * x.Quantity)
        };

        await _sendEndpointCustomProvider.SendMessage<ICheckoutBasket>(message, cancellationToken);
        _logger.LogInformation("Basket checked out. BasketId: {BasketId}. Waiting to create order, decrease stock.",
            basket.BasketId);
    }

    // private async Task UpdateBasketItemsPriceAndStock(IEnumerable<Data.Models.BasketItem> basketItems)
    // {
    //     var productIds = basketItems.Select(x => x.ProductId).ToList();
    //     var response = await _catalogService.GetProductsByIds(productIds);
    //     if (response == null) throw new ResourceNotFoundException(nameof(ProductDto), "ProductIds");
    //     var products = response.Payload;
    //     foreach (var basketItem in basketItems)
    //     {
    //         var product = products.FirstOrDefault(x => x.Id == basketItem.ProductId);
    //         if (product == null) throw new ResourceNotFoundException(nameof(ProductDto), basketItem.ProductId.ToString());
    //         if (basketItem.Product.UnitPrice != product.Price) basketItem.Product.UnitPrice = product.Price;
    //         if (basketItem.Product.Stock != product.Stock) basketItem.Product.Stock = product.Stock;
    //     }
    // }
}