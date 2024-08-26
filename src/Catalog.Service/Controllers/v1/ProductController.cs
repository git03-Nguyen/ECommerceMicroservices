using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;
using Catalog.Service.Features.Queries.ProductQueries.GetAllProducts;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Models.Requests;
using MediatR;
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var request = new GetAllProductsRequest(page, pageSize);
        var query = new GetAllProductsQuery(request);
        var products = await _mediator.Send(query, cancellationToken);
        return Ok(products);
    }

    // TODO: what if the request is not always an integer?
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest(id);
        var query = new GetProductByIdQuery(request);
        var product = await _mediator.Send(query, cancellationToken);
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNewProductRequest request)
    {
        var command = new CreateNewProductCommand(request);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

}