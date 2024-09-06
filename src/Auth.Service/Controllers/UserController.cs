using Auth.Service.Features.Commands.UserCommands.DeleteUser;
using Auth.Service.Features.Commands.UserCommands.LogIn;
using Auth.Service.Features.Commands.UserCommands.ResetPassword;
using Auth.Service.Features.Commands.UserCommands.RevokeToken;
using Auth.Service.Features.Commands.UserCommands.SignUp;
using Auth.Service.Features.Commands.UserCommands.UpdateUser;
using Auth.Service.Features.Queries.UserQueries.GetAllUsers;
using Auth.Service.Features.Queries.UserQueries.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignupRequest request)
    {
        var response = await _mediator.Send(new SignUpCommand(request));
        return Created("", response);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LogInRequest request)
    {
        var response = await _mediator.Send(new LogInCommand(request));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        // TODO: Implement ResetPasswordCommand
        var response = await _mediator.Send(new ResetPasswordCommand(request));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
    {
        // TODO: Implement RevokeTokenCommand
        var response = await _mediator.Send(new RevokeTokenCommand(request));
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllUsersQuery());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetByEmail([FromQuery] GetUserByEmailRequest request)
    {
        var response = await _mediator.Send(new GetUserByEmailQuery(request));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request)
    {
        var response = await _mediator.Send(new DeleteUserCommand(request));
        return NoContent();
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var response = await _mediator.Send(new UpdateUserCommand(request));
        return Ok(response);
    }
}