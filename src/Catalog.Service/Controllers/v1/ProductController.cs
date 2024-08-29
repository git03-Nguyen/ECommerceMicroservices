using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Features.Queries.ProductQueries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers.v1;


[ApiController]
[Route("api/v1/[controller]/[action]")]
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
    public async Task<IActionResult> Get([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetProductsQuery(request), cancellationToken);
        return Ok(products);
    }

    // TODO: what if the request is not always an integer?
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(product);
    }
    
    [HttpPost]
    [Authorize("AdminOnly")]
    public async Task<IActionResult> Add([FromBody] AddNewProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new AddNewProductCommand(request), cancellationToken);
        return Created($"/api/v1/Product/GetById/{product.ProductId}", product);
    }
    

}