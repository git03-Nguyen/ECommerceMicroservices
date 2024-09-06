using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Events;
using MediatR;
using Order.Service.Data.Models;
using Order.Service.Repositories;

namespace Order.Service.Features.Commands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand>
{
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly IPublishEndpointCustomProvider _publishEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger,
        IPublishEndpointCustomProvider publishEndpointCustomProvider, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _publishEndpointCustomProvider = publishEndpointCustomProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        // Create order
        var order = new Data.Models.Order
        {
            BuyerId = request.Payload.AccountId,
            RecipientName = request.Payload.RecipientName,
            ShippingAddress = request.Payload.ShippingAddress,
            RecipientPhone = request.Payload.RecipientPhone,
            OrderItems = new List<OrderItem>(),
            Status = OrderStatus.Pending
        };

        // Add order items
        order.OrderItems = request.Payload.BasketItems.Select(x => new OrderItem
        {
            OrderId = order.OrderId,
            ProductId = x.ProductId,
            SellerAccountId = x.SellerAccountId,
            ProductName = x.ProductName,
            ProductImageUrl = x.ImageUrl,
            ProductPrice = x.UnitPrice,
            Quantity = x.Quantity
        }).ToList();

        // Save order
        order.TotalPrice = order.OrderItems.Sum(x => x.ProductPrice * x.Quantity);
        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish order created event
        var message = new OrderCreated
        {
            BasketId = request.Payload.BasketId,
            OrderItems = order.OrderItems.Select(x => new OrderItemCreated
            {
                ProductId = x.ProductId,
                SellerAccountId = x.SellerAccountId,
                Quantity = x.Quantity
            }).ToList()
        };
        message.TotalPrice = order.TotalPrice;
        await _publishEndpointCustomProvider.PublishMessage<OrderCreated>(message, cancellationToken);
        _logger.LogInformation("Order created. OrderId: {OrderId}", order.OrderId);
    }
}