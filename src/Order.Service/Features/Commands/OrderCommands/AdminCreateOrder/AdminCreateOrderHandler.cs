using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Messages.Events;
using Contracts.Services.Identity;
using MediatR;
using Order.Service.Data.Models;
using Order.Service.Repositories;

namespace Order.Service.Features.Commands.OrderCommands.AdminCreateOrder;

public class AdminCreateOrderHandler : IRequestHandler<AdminCreateOrderCommand, AdminCreateOrderResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<AdminCreateOrderHandler> _logger;
    private readonly IPublishEndpointCustomProvider _publishEndpointCustomProvider;
    private readonly IUnitOfWork _unitOfWork;


    public AdminCreateOrderHandler(ILogger<AdminCreateOrderHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService, IPublishEndpointCustomProvider publishEndpointCustomProvider)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _publishEndpointCustomProvider = publishEndpointCustomProvider;
    }

    public async Task<AdminCreateOrderResponse> Handle(AdminCreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        // Check admin
        _identityService.EnsureIsAdmin();

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
        await _publishEndpointCustomProvider.PublishMessage(message, cancellationToken);
        _logger.LogInformation("Order created. OrderId: {OrderId}", order.OrderId);

        return new AdminCreateOrderResponse(order);
    }
}
