using MediatR;
using Order.Service.Data.Models;
using Order.Service.Repositories;

namespace Order.Service.Features.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // var order = new Order.Service.Data.Models.Order
        // {
        //     OrderDate = DateTime.Now,
        //     OrderItems = request.OrderItems.Select(x => new OrderItem
        //     {
        //         ProductId = x.ProductId,
        //         Quantity = x.Quantity
        //     }).ToList()
        // };
        //
        // await _unitOfWork.OrderRepository.AddAsync(order);
        // await _unitOfWork.SaveChangesAsync();
        //
        // return new CreateOrderResponse();
        return new();
        
    }
}