using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Service.Features.Commands.Product.CreateNewProduct;
using Product.Service.Features.Commands.Product.DeleteAProduct;
using Product.Service.Features.Queries.GetAllProducts;
using Product.Service.Features.Queries.GetProductById;
using Product.Service.Models;

namespace Product.Service.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductItem productItem)
    {
        var product =
            await _mediator.Send(new CreateNewProductCommand(productItem.Name, productItem.Description,
                productItem.Price));
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) return NotFound();

        await _mediator.Send(new DeleteAProductCommand(id));
        return NoContent();
    }
}