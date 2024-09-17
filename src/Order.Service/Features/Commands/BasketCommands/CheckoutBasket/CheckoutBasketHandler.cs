using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events.Order;
using MassTransit;
using MediatR;
using Order.Service.Data.Models;
using Order.Service.Repositories;

namespace Order.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand>
{
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
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
            SellerAccountName = x.SellerAccountName,
            ProductName = x.ProductName,
            ProductImageUrl = x.ImageUrl,
            ProductPrice = x.UnitPrice,
            Quantity = x.Quantity
        }).ToList();

        // Save order
        order.TotalPrice = order.OrderItems.Sum(x => x.ProductPrice * x.Quantity);
        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish order created event to: Catalog, Notification
        await PublishOrderCreatedEvent(order, request.Payload, cancellationToken);
    }

    private async Task PublishOrderCreatedEvent(Data.Models.Order order, ICheckoutBasket request,
        CancellationToken cancellationToken)
    {
        var message = new
        {
            request.BasketId,
            OrderItems = order.OrderItems.Select(x => new
            {
                x.ProductId,
                x.SellerAccountId,
                x.SellerAccountName,
                x.Quantity
            }).ToList(),
            order.TotalPrice
        };
        await _publishEndpoint.Publish<IOrderCreated>(message, cancellationToken);
        _logger.LogInformation("Order created. OrderId: {OrderId}", order.OrderId);
    }
}