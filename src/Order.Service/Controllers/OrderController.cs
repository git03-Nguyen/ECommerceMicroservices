using Contracts.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Features.Queries.OrderQueries.AdminGetAllOrders;
using Order.Service.Features.Queries.OrderQueries.GetOwnOrders;

namespace Order.Service.Controllers;

[ApiController]
[Authorize(CustomPolicyNameConstants.CustomerOrSeller)]
[Route("api/v1/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(CustomPolicyNameConstants.AdminOnly)]
    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AdminGetAllOrdersQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetOwnOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOwnOrdersQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        // TODO: Only delete when Delivered or Cancelled
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateShippingInfo()
    {
        // TODO: Update the Shipping info only when status is PENDING
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateOrderStatus()
    {
        // TODO
        // 1. PENDING -> SHIPPING: Seller accepts the order
        // 3. SHIPPING -> DELIVERED: Customer receives the order
        // 4. SHIPPING -> CANCELLED: Seller cancel the order or Customer do not receive the order
        // 6. PENDING -> CANCELLED: Seller, Customer cancel the order
        // In General, the former status can be changed into the latter status, but not vice versa.
        // When the status is changed to CANCELLED, the quantity of the product should be restored.
        return Ok();
    }
}