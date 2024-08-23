using Customer.Service.Data;
using Customer.Service.Features.Commands.CreateCustomer;
using Customer.Service.Features.Commands.DeleteCustomer;
using Customer.Service.Features.Queries.GetAllCustomers;
using Customer.Service.Features.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Service.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize("ClientIdPolicy")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IMediator _mediator;

    public CustomerController(ILogger<CustomerController> logger, CustomerContext context, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Customer customer, CancellationToken cancellationToken)
    {
        var createdCustomer = await _mediator.Send(new CreateCustomerCommand(customer), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
        return NoContent();
    }
}