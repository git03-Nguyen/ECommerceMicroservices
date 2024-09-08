using Auth.Service.Exceptions;
using Auth.Service.Features.Commands.UserCommands.SignUp;
using Auth.Service.Tests.Extensions;
using Contracts.MassTransit.Core.PublishEndpoint;

namespace Auth.Service.Tests.Features.Commands.UserCommands.SignUp;

[TestFixture]
public class SignUpHandlerTests
{
    private Mock<ILogger<SignUpHandler>> _logger;
    private Mock<IPublishEndpointCustomProvider> _publishEndpointCustomProvider;
    private Mock<RoleManager<ApplicationRole>> _roleManager;
    private Mock<UserManager<ApplicationUser>> _userManager;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private SignUpHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<SignUpHandler>>();
        _publishEndpointCustomProvider = new Mock<IPublishEndpointCustomProvider>();
        _roleManager = new Mock<RoleManager<ApplicationRole>>(
            Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
        _userManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new SignUpHandler(_logger.Object, _userManager.Object, _roleManager.Object, _publishEndpointCustomProvider.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldReturnSignUpResponse()
    {
        // Arrange
        var request = new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        };
        var command = new SignUpCommand(request);

        _userManager.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);
        _userManager.Setup(x => x.FindByNameAsync(request.UserName)).ReturnsAsync((ApplicationUser)null);
        _roleManager.Setup(x => x.RoleExistsAsync(request.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), request.Role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<SignUpResponse>());
        Assert.That(result.UserName, Is.EqualTo(request.UserName));
        Assert.That(result.Email, Is.EqualTo(request.Email));
    }
    
    [Test]
    public async Task Handle_WhenUserExistsByEmail_ButDeleted_ShouldReturnSignUpResponse()
    {
        // Arrange
        var request = new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        };
        var command = new SignUpCommand(request);

        _userManager.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(new ApplicationUser { IsDeleted = true });
        _userManager.Setup(x => x.FindByNameAsync(request.UserName)).ReturnsAsync((ApplicationUser)null);
        _roleManager.Setup(x => x.RoleExistsAsync(request.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), request.Role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<SignUpResponse>());
        Assert.That(result.UserName, Is.EqualTo(request.UserName));
        Assert.That(result.Email, Is.EqualTo(request.Email));
    }
    
    [Test]
    public async Task Handle_WhenUserExistsByUsername_ButDeleted_ShouldReturnSignUpResponse()
    {
        // Arrange
        var request = new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        };
        var command = new SignUpCommand(request);

        _userManager.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);
        _userManager.Setup(x => x.FindByNameAsync(request.UserName)).ReturnsAsync(new ApplicationUser { IsDeleted = true });
        _roleManager.Setup(x => x.RoleExistsAsync(request.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), request.Role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<SignUpResponse>());
        Assert.That(result.UserName, Is.EqualTo(request.UserName));
        Assert.That(result.Email, Is.EqualTo(request.Email));
    }

    [Test]
    public async Task Handle_WhenRoleDoesNotExist_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        var command = new SignUpCommand(new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        });

        _roleManager.Setup(x => x.RoleExistsAsync(command.Payload.Role)).ReturnsAsync(false);

        // Act and Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(command, _cancellationToken));

    }
    
    [Test]
    public async Task Handle_WhenUserExistsByEmail_ShouldThrowResourceAlreadyExistsException()
    {
        // Arrange
        var command = new SignUpCommand(new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        });
        
        _roleManager.Setup(x => x.RoleExistsAsync(command.Payload.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.FindByEmailAsync(command.Payload.Email)).ReturnsAsync(new ApplicationUser { IsDeleted = false });

        // Act and Assert
        Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _handler.Handle(command, _cancellationToken));
    }
    
    [Test]
    public async Task Handle_WhenUserExistsByUsername_ShouldThrowResourceAlreadyExistsException()
    {
        // Arrange
        var command = new SignUpCommand(new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        });
        
        _roleManager.Setup(x => x.RoleExistsAsync(command.Payload.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.FindByEmailAsync(command.Payload.Email)).ReturnsAsync((ApplicationUser)null);
        _userManager.Setup(x => x.FindByNameAsync(command.Payload.UserName)).ReturnsAsync(new ApplicationUser { IsDeleted = false });

        // Act and Assert
        Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _handler.Handle(command, _cancellationToken));
    }
    
    [Test]
    public async Task Handle_WhenCreateUserFails_ShouldThrowException()
    {
        // Arrange
        var command = new SignUpCommand(new SignUpRequest
        {
            UserName = "username",
            Email = "email",
            Password = "password",
            Role = "role"
        });
        
        _roleManager.Setup(x => x.RoleExistsAsync(command.Payload.Role)).ReturnsAsync(true);
        _userManager.Setup(x => x.FindByEmailAsync(command.Payload.Email)).ReturnsAsync((ApplicationUser)null);
        _userManager.Setup(x => x.FindByNameAsync(command.Payload.UserName)).ReturnsAsync((ApplicationUser)null);
        _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Payload.Password)).ReturnsAsync(IdentityResult.Failed());

        // Act and Assert
        Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, _cancellationToken));
    }

}