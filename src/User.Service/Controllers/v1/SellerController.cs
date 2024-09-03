using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;
using User.Service.Features.Queries.SellerQueries.GetAllSellers;
using User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

namespace User.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class SellerController : ControllerBase
{
    private readonly IMediator _mediator;

    public SellerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllSellersQuery());
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetByEmail([FromBody] GetSellerByEmailRequest request)
    {
        var response = await _mediator.Send(new GetSellerByEmailQuery(request));
        return Ok(response);
    }
    
    
}