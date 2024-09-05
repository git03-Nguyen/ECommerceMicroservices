using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.Features.Commands.CustomerCommands.UpdateCustomer;
using User.Service.Features.Queries.CustomerQueries.GetAllCustomers;
using User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

namespace User.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetByEmail([FromBody] GetCustomerByEmailRequest request)
    {
        var response = await _mediator.Send(new GetCustomerByEmailQuery(request));
        return Ok(response);
    }
    
    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest request)
    {
        var response = await _mediator.Send(new UpdateCustomerCommand(request));
        return Ok(response);
    }
    
}