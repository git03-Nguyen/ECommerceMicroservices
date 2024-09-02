using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Order.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMediator _mediator;

    public OrderController(ILogger<OrderController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // [HttpPost]
    // public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    // {
    //     var result = await _mediator.Send(new CreateOrderCommand(request));
    //     return Ok(result);
    // }
}