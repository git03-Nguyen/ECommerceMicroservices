using Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;
using Basket.Service.Features.Commands.BasketCommands.UpdateItem;
using Basket.Service.Features.Queries.BasketQueries.GetAllBaskets;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllBasketsQuery());
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetBasketOfACustomerRequest request)
    {
        var response = await _mediator.Send(new GetBasketOfACustomerQuery(request));
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateItemRequest request)
    {
        await _mediator.Send(new UpdateItemCommand(request));
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutBasketRequest request)
    {
        await _mediator.Send(new CheckoutBasketCommand(request));
        return Ok();
    }
}