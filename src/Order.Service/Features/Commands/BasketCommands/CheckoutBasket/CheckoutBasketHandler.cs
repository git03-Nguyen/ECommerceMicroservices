using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using Order.Service.Data.Models;
using Order.Service.Repositories;

namespace Order.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketHandler : IRequestHandler<CheckoutBasketCommand>
{
    private readonly ILogger<CheckoutBasketHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    public CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
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
    
    private async Task PublishOrderCreatedEvent(Data.Models.Order order, Contracts.MassTransit.Messages.Commands.ICheckoutBasket request, CancellationToken cancellationToken)
    {
        var message = new 
        {
            BasketId = request.BasketId,
            OrderItems = order.OrderItems.Select(x => new
            {
                ProductId = x.ProductId,
                SellerAccountId = x.SellerAccountId,
                SellerAccountName = x.SellerAccountName,
                Quantity = x.Quantity
            }).ToList(),
            TotalPrice = order.TotalPrice
        };
        await _publishEndpoint.Publish<IOrderCreated>(message, cancellationToken);
        _logger.LogInformation("Order created. OrderId: {OrderId}", order.OrderId);
    }
}