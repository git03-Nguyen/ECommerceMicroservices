using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.CategoryQueries.GetAllCategories;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CatergoryController : Controller
{
    private readonly ILogger<CatergoryController> _logger;
    private readonly IMediator _mediator;
    
    public CatergoryController(ILogger<CatergoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var request = new GetAllCategoriesRequest(page, pageSize);
        var query = new GetAllCategoriesQuery(request);
        var categories = await _mediator.Send(query, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var request = new GetCategoryByIdRequest(id);
        var query = new GetCategoryByIdQuery(request);
        var category = await _mediator.Send(query, cancellationToken);
        return Ok(category);
    }
    
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] Category category)
    // {
    //     var createdCategory = await _mediator.Send(new CreateCategoryCommand(category));
    //     return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    // }
    
}