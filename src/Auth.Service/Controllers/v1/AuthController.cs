using Auth.Service.Features.Commands.UserCommands.LogIn;
using Auth.Service.Features.Commands.UserCommands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
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
}