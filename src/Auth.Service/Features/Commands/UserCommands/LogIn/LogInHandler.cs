using Auth.Service.Data.Models;
using Auth.Service.Options;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInHandler : IRequestHandler<LogInCommand, LogInResponse>
{
    private readonly ILogger<LogInHandler> _logger;
    private readonly IOptions<AuthOptions> _options;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public LogInHandler(ILogger<LogInHandler> logger, IOptions<AuthOptions> options, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _options = options;
        _userManager = userManager;
    }

    public async Task<LogInResponse> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var username = request.Payload.UserName;
        var password = request.Payload.Password;
        
        // Validate username and password
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            throw new AuthenticationFailureException("User not found");
        }
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new AuthenticationFailureException("Invalid password");
        }

        // NOTE: this implements the Resource Owner Password Grant flow of OAuth 2.0
        var authOptions = _options.Value;
        var grantType = authOptions.GrantType;
        var clientId = authOptions.ClientId;
        var clientSecret = authOptions.ClientSecret;
        var scope = authOptions.Scope;
        var identityServerUrl = authOptions.IdentityServerUrl;
        var identityServerTokenEndpoint = authOptions.IdentityServerTokenEndpoint;

        // Redirect to /connect/token with client_id, client_secret, grant_type, username, password as x-www-form-urlencoded
        using var client = new HttpClient();
        client.BaseAddress = new Uri(identityServerUrl);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, identityServerTokenEndpoint);
        httpRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
           { "grant_type", grantType },
           { "client_id", clientId },
           { "client_secret", clientSecret },
           { "username", username },
           { "password", password },
           { "scope", scope }
        });

        var response = await client.SendAsync(httpRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("{username} - Failed to get token from Identity Server", username);
            throw new Exception("Failed to get token from Identity Server");
        }

        // Response is a json object with access_token, token_type, expires_in, refresh_token, scope
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return new LogInResponse(responseContent);
    }
}