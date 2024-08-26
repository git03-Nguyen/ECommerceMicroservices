using Auth.Service.Data.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticateController : ControllerBase
{
    private readonly ILogger<AuthenticateController> _logger;

    public AuthenticateController(ILogger<AuthenticateController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var account = loginRequest.Account;
        var password = loginRequest.Password;
        // ........

        return Unauthorized();
    }
}