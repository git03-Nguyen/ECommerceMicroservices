

using System.Net;
using Auth.Service.Features.Commands.UserCommands.LogIn;
using Auth.Service.Options;
using Auth.Service.Tests.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;

namespace Auth.Service.Tests.Features.Commands.UserCommands.Login;

[TestFixture]
public class LoginHandlerTests
{
    private Mock<UserManager<ApplicationUser>> _userManager;
    private Mock<ILogger<LogInHandler>> _logger;
    private Mock<IOptions<AuthOptions>> _options;
    private Mock<IHttpClientFactory> _httpClientFactory;
    private LogInHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void SetUp()
    {
        _userManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _logger = new Mock<ILogger<LogInHandler>>();
        _options = new Mock<IOptions<AuthOptions>>();
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _cancellationToken = new CancellationToken();
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _handler = new LogInHandler(_logger.Object, _options.Object, _userManager.Object, _httpClientFactory.Object);
    }

    [Test]
    public void Handle_WhenUserNotFound_ThrowsAuthenticationFailureException()
    {
        // Arrange
        var request = new LogInRequest
        {
            UserName = "username",
            Password = "password"
        };
        var command = new LogInCommand(request);
        _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

        // Act & Assert
        Assert.ThrowsAsync<AuthenticationFailureException>(async () =>
        {
            await _handler.Handle(command, _cancellationToken);
        });
    }

    [Test]
    public void Handle_WhenPasswordIsInvalid_ThrowsAuthenticationFailureException()
    {
        // Arrange
        var request = new LogInRequest
        {
            UserName = "username",
            Password = "password"
        };
        var command = new LogInCommand(request);
        _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
        _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act & Assert
        Assert.ThrowsAsync<AuthenticationFailureException>(async () =>
        {
            await _handler.Handle(command, _cancellationToken);
        });
    }

    [Test]
    public async Task Handle_WhenUserFound_ReturnsLogInResponse()
    {
        // Arrange
        var request = new LogInRequest
        {
            UserName = "username",
            Password = "password"
        };
        var command = new LogInCommand(request);
        var user = _fixture.Build<ApplicationUser>().With(x => x.IsDeleted, false).Create();
        _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        _options.Setup(x => x.Value).Returns(new AuthOptions
        {
            GrantType = "password",
            ClientId = "client_id",
            ClientSecret = "client_secret",
            Scope = "scope",
            IdentityServerUrl = "http://localhost:16100",
            IdentityServerTokenEndpoint = "http://localhost:16100/connect/token"
        });
        
        var httpClient = new Mock<HttpClient>();
        _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient.Object);
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var accessToken = _fixture.Create<string>();
        var tokenType = _fixture.Create<string>();
        var expiresIn = _fixture.Create<int>();
        var refreshToken = _fixture.Create<string>();
        var scope = _fixture.Create<string>();
        response.Content = new StringContent($@"
        {{
            ""access_token"": ""{accessToken}"",
            ""token_type"": ""{tokenType}"",
            ""expires_in"": {expiresIn},
            ""refresh_token"": ""{refreshToken}"",
            ""scope"": ""{scope}""
        }}");
        httpClient.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), _cancellationToken))
            .ReturnsAsync(response);       

        // Act
        var result = await _handler.Handle(command, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AccessToken, Is.EqualTo(accessToken));
        Assert.That(result.TokenType, Is.EqualTo(tokenType));
        Assert.That(result.ExpiresIn, Is.EqualTo(expiresIn));
        
        _httpClientFactory.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Once);
        _userManager.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
        _userManager.Verify(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }
}
