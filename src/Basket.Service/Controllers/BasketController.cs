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
    public IActionResult GetAll()
    {
        var response = _mediator.Send(new GetAllBasketsQuery());
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Get([FromBody] GetBasketOfACustomerRequest request)
    {
        var response = _mediator.Send(new GetBasketOfACustomerQuery(request));
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Update([FromBody] UpdateItemRequest request)
    {
        _mediator.Send(new UpdateItemCommand(request));
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout([FromBody] CheckoutBasketRequest request)
    {
        _mediator.Send(new CheckoutBasketCommand(request));
        return Ok();
    }
}