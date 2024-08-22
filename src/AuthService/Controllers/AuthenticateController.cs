using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers;

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

        if (account == "admin" && password == "admin")
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, account),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                "This is my test private key. This is my test private key. This is my test private key."));

            var jwt = new JwtSecurityToken(
                "http://localhost:5000",
                "http://localhost:5000",
                claims,
                now,
                now.Add(TimeSpan.FromMinutes(60)),
                new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(60).TotalSeconds
            };

            return Ok(responseJson);
        }

        return Unauthorized();
    }
}