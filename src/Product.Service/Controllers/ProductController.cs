using Microsoft.AspNetCore.Mvc;
using Product.Service.Models;

namespace Product.Service.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            new List<ProductItem>
            {
                new()
                {
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 100
                },
                new()
                {
                    Name = "Product 2",
                    Description = "Description 2",
                    Price = 200
                }
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(
            new ProductItem
            {
                Name = "Product 1",
                Description = "Description 1",
                Price = 100
            }
        );
    }
}