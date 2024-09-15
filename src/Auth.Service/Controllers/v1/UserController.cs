using Auth.Service.Features.Commands.UserCommands.ChangePassword;
using Auth.Service.Features.Commands.UserCommands.DeleteUser;
using Auth.Service.Features.Commands.UserCommands.LogIn;
using Auth.Service.Features.Commands.UserCommands.SignUp;
using Auth.Service.Features.Queries.UserQueries.GetOwnProfileQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await _mediator.Send(new ChangePasswordCommand(request));
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOwnProfile()
    {
        var response = await _mediator.Send(new GetOwnProfileQuery());
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }


    // TODO: Verification, Reset/Forgot Password, SignOut, Revoke Token, Refresh Token, Access Token Abuse?
    // [HttpPost]
    // public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    // {
    //     // TODO: Implement ResetPasswordCommand
    //     var response = await _mediator.Send(new ResetPasswordCommand(request));
    //     return Ok(response);
    // }
    // [HttpPost]
    // public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
    // {
    //     var response = await _mediator.Send(new RevokeTokenCommand(request));
    //     return Ok(response);
    // }
    
}