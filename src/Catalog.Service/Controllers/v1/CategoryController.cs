using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;
using Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;
using Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategories;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Models.Dtos;
using Catalog.Service.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    
    public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return Ok(category);
    }
    
    [Authorize("AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddNewCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(new AddNewCategoryCommand(request), cancellationToken);
        return Created($"/api/v1/Category/GetById/{category.CategoryId}", category);
    }
    
    [Authorize("AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }
    
    [Authorize("AdminOnly")]
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(new UpdateCategoryCommand(request), cancellationToken);
        return Ok(category);
    }
    
}