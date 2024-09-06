using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Features.Commands.AdminCreateOrder;

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
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] AdminCreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AdminCreateOrderCommand(request), cancellationToken);
        return Ok(response);
    }
}