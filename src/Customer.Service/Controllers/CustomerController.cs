using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            new List<Models.Customer>
            {
                new()
                {
                    Id = 1, Name = "John Doe", Email = "mail@mail.com", Phone = "1234567890", Address = "123 Main St"
                },
                new()
                {
                    Id = 2, Name = "Jane Doe", Email = "mkok@mail.com", Phone = "0987654321", Address = "456 Main St"
                }
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(
            new Models.Customer
            {
                Id = id, Name = "John Doe", Email = "mail@mail.com", Phone = "1234567890", Address = "123 Main St"
            }
        );
    }
}