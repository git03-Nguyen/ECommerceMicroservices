using Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;
using Basket.Service.Features.Commands.BasketCommands.DecreaseItem;
using Basket.Service.Features.Commands.BasketCommands.IncreaseItem;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Contracts.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(Policy = CustomPolicyNameConstants.CustomerOnly)]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetBasketOfACustomerQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Increase([FromBody] UpdateItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new IncreaseItemCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Decrease([FromBody] UpdateItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DecreaseItemCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutBasketRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CheckoutBasketCommand(request), cancellationToken);
        return Ok(response);
    }
}