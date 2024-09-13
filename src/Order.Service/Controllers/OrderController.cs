using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Features.Commands.OrderCommands.AdminCreateOrder;
using Order.Service.Features.Queries.OrderQueries.AdminGetAllOrders;
using Order.Service.Features.Queries.OrderQueries.GetOwnOrders;

namespace Order.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AdminGetAllOrdersQuery(), cancellationToken);
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOwnOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOwnOrdersQuery(), cancellationToken);
        return Ok(response);
    }
    
    // [Authorize]
    // [HttpGet]
    // public async Task<IActionResult> GetOrderById([FromQuery] AdminGetOrderByIdRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     var response = await _mediator.Send(new AdminGetOrderByIdQuery(request), cancellationToken);
    //     return Ok(response);
    // }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] AdminCreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AdminCreateOrderCommand(request), cancellationToken);
        return Ok(response);
    }
}