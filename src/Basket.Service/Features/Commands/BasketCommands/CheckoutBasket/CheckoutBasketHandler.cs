using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Messages.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IIdentityService identityService,
        IUnitOfWork unitOfWork, ISendEndpointCustomProvider sendEndpointCustomProvider)
    {
        _logger = logger;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
    }

    public async Task Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByCondition(x => x.BasketId == request.Payload.BasketId)
            .FirstOrDefaultAsync(cancellationToken);
        if (basket == null) throw new ResourceNotFoundException("BasketId", request.Payload.BasketId.ToString());

        // Check if owner of the basket is the same as the user
        var isOwner = _identityService.IsResourceOwner(basket.AccountId);
        if (!isOwner) throw new UnAuthorizedAccessException();

        // Check if basket is empty
        if (basket.BasketItems.Count == 0) throw new BasketEmptyException(request.Payload.BasketId);

        // TODO: Update all products' price and stock in the basket by syncing with the product service
        // ...

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
        var message = new Contracts.MassTransit.Messages.Commands.CheckoutBasket
        {
            BasketId = request.Payload.BasketId,
            AccountId = basket.AccountId,
            RecipientName = request.Payload.RecipientName,
            ShippingAddress = request.Payload.ShippingAddress,
            RecipientPhone = request.Payload.RecipientPhone,
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
            }).ToList()
        };
        await _sendEndpointCustomProvider
            .SendMessage<Contracts.MassTransit.Messages.Commands.CheckoutBasket>(message, cancellationToken);
        _logger.LogInformation("Basket checked out. BasketId: {BasketId}. Waiting to create order.",
            request.Payload.BasketId);

        // Consume the response from the order service to clear the basket (in other consumer: OrderCreatedConsumer)
    }
}
