using Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;
using Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;
using Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;
using Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;
using Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;
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
    public async Task<IActionResult> GetByIds([FromBody] GetManyProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetManyProductsQuery(request), cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOrSeller")]
    public async Task<IActionResult> Add([FromBody] AddNewProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new AddNewProductCommand(request), cancellationToken);
        return Created($"/api/v1/Product/GetById/{product.ProductId}", product);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProductCommand(request), cancellationToken);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }

    // Get prices and stocks of products
    [HttpPost]
    public async Task<IActionResult> GetPricesAndStocks([FromBody] GetPricesAndStocksRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPricesAndStocksQuery(request), cancellationToken);
        return Ok(response);
    }
}