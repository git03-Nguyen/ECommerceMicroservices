using System.Text.Json;
using Auth.Service.Data.Models;
using Auth.Service.Features.Commands.AuthCommands.SignUp;
using Auth.Service.Models.Requests;
using Auth.Service.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LoginRequest = Auth.Service.Models.Requests.LoginRequest;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(ILogger<AuthController> logger, SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _logger = logger;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignupRequest request)
    {
        var response = await _mediator.Send(new SignUpCommand(request));
        return CreatedAtAction(nameof(SignUp), response);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest,
        [FromServices] IOptions<AuthOptions> options)

    {
        var username = loginRequest.Username;
        var password = loginRequest.Password;

        // NOTE: this implements the Resource Owner Password Grant flow of OAuth 2.0
        var authOptions = options.Value;
        var grantType = authOptions.GrantType;
        var clientId = authOptions.ClientId;
        var clientSecret = authOptions.ClientSecret;
        var scope = authOptions.Scope;
        var identityServerUrl = authOptions.IdentityServerUrl;
        var identityServerTokenEndpoint = authOptions.IdentityServerTokenEndpoint;

        // Redirect to /connect/token with client_id, client_secret, grant_type, username, password as x-www-form-urlencoded
        var client = new HttpClient();
        client.BaseAddress = new Uri(identityServerUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, identityServerTokenEndpoint);
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", grantType },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "username", username },
            { "password", password },
            { "scope", scope }
        });

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode) return BadRequest("Invalid username or password");

        // Response is a json object with access_token, token_type, expires_in, refresh_token, scope
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseContent);

        return Ok(responseJson);
    }
    
    [HttpPost]
    public async Task<IActionResult> Revoke()
    {
        // TODO
        return Ok();
    }
}