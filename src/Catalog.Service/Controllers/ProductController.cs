using Catalog.Service.Features.Queries.ProductQueries.GetAllProducts;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    
    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

}