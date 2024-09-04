using Auth.Service.Data.Models;
using Auth.Service.Features.Commands.RoleCommands.AddNewRole;
using Auth.Service.Features.Commands.RoleCommands.DeleteRole;
using Auth.Service.Features.Commands.RoleCommands.UpdateRole;
using Auth.Service.Features.Queries.RoleQueries.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllRolesQuery());
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddNewRoleRequest request)
    {
        var response = await _mediator.Send(new AddNewRoleCommand(request));
        return CreatedAtAction("", response);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRoleRequest request)
    {
        var response = await _mediator.Send(new DeleteRoleCommand(request));
        return NoContent();
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request)
    {
        var response = await _mediator.Send(new UpdateRoleCommand(request));
        return Ok(response);
    }
    
    // TODO: Assign roles to users
    
    
    // TODO: Remove roles from users
    
}