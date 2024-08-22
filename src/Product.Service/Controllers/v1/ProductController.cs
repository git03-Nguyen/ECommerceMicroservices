using Microsoft.AspNetCore.Mvc;
using Product.Service.Models;

namespace Product.Service.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
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
                new ProductItem
                {
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 100
                },
                new ProductItem
                {
                    Name = "Product 2",
                    Description = "Description 2",
                    Price = 200
                }
            }
            );
    }

}