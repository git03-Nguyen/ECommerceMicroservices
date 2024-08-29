using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Service.Data.Models;
using Auth.Service.Data.Models.Requests;
using Authentication.Service.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = AutoMapper.Configuration.IConfiguration;
using LoginRequest = Auth.Service.Data.Models.Requests.LoginRequest;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticateController : ControllerBase
{
    private readonly ILogger<AuthenticateController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateController(ILogger<AuthenticateController> logger, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userManager = userManager;
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        var role = new ApplicationRole
        {
            Name = roleName
        };
        
        var result = await _roleManager.CreateAsync(role);
        
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return Created("/AuthService/", role);
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignupRequest request)
    {
        var email = request.Email;
        var password = request.Password;
        var role = request.Role;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
        };
        
        var existingUser = await _userManager.FindByEmailAsync(email);
        
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }
        
        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        // Add user to registered role
        result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return Created("/AuthService/", user);
        
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, [FromServices] AuthConfiguration authConfiguration)
    {
        var username = loginRequest.Username;
        var password = loginRequest.Password;
        
        // NOTE: this implements the Resource Owner Password Grant flow of OAuth 2.0
        var grantType = authConfiguration.GrantType;
        var clientId = authConfiguration.ClientId;
        var clientSecret = authConfiguration.ClientSecret;
        var scope = authConfiguration.Scope;
        var identityServerUrl = authConfiguration.IdentityServerUrl;
        var identityServerTokenEndpoint = authConfiguration.IdentityServerTokenEndpoint;
        
        // Redirect to /connect/token with client_id, client_secret, grant_type, username, password as x-www-form-urlencoded
        var client = new HttpClient();
        client.BaseAddress = new Uri(identityServerUrl);
        
        var request = new HttpRequestMessage(HttpMethod.Post, identityServerTokenEndpoint);
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", grantType},
            {"client_id", clientId},
            {"client_secret", clientSecret},
            {"username", username},
            {"password", password},
            {"scope", scope}
        });
        
        var response = await client.SendAsync(request);
        
        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Invalid username or password");
        }
        
        // Response is a json object with access_token, token_type, expires_in, refresh_token, scope
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = System.Text.Json.JsonDocument.Parse(responseContent);
        
        return Ok(responseJson);
    }
}