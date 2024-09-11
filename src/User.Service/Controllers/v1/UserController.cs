using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.Features.Commands.UserCommands.UpdateUser;
using User.Service.Features.Queries.UserQueries.GetAllUsers;
using User.Service.Features.Queries.UserQueries.GetOwnProfile;
using User.Service.Features.Queries.UserQueries.GetUserById;
using User.Service.Features.Queries.UserQueries.GetUsersByEmail;

namespace User.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOwnProfile()
    {
        var response = await _mediator.Send(new GetOwnProfileQuery());
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllUsersQuery());
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> GetByEmail([FromBody] GetUserByEmailRequest request)
    {
        var response = await _mediator.Send(new GetUserByEmailQuery(request));
        return Ok(response);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var response = await _mediator.Send(new UpdateUserCommand(request));
        return Ok(response);
    }

}