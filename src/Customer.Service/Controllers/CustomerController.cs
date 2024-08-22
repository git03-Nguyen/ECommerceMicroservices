using Customer.Service.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize("ClientIdPolicy")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerContext _context;

    public CustomerController(ILogger<CustomerController> logger, CustomerContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return _context.Customers.Any()
            ? Ok(await _context.Customers.ToListAsync())
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        return customer is not null
            ? Ok(customer)
            : NotFound();
    }
}