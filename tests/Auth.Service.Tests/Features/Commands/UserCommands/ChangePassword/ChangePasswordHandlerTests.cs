using Auth.Service.Features.Commands.UserCommands.ChangePassword;

namespace Auth.Service.Tests.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordHandlerTests
{
    private CancellationToken _cancellationToken;
    private ChangePasswordCommand _command;
    private Fixture _fixture;

    private ChangePasswordHandler _handler;
    private Mock<IIdentityService> _identityService;
    private Mock<ILogger<ChangePasswordHandler>> _logger;
    private Mock<UserManager<ApplicationUser>> _userManager;

    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<ChangePasswordHandler>>();
        _userManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _identityService = new Mock<IIdentityService>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _handler = new ChangePasswordHandler(_logger.Object, _userManager.Object, _identityService.Object);
        _command = new ChangePasswordCommand(new ChangePasswordRequest
        {
            Password = "password",
            NewPassword = "newPassword"
        });
        _cancellationToken = new CancellationToken();

        var userInfo = _fixture.Create<IdentityDto>();
        _identityService.Setup(x => x.GetUserInfoIdentity()).Returns(userInfo);
        var user = _fixture.Create<ApplicationUser>();
        _userManager.Setup(x => x.FindByIdAsync(userInfo.Id)).ReturnsAsync(user);
        _userManager.Setup(x => x.ChangePasswordAsync(user, _command.Payload.Password, _command.Payload.NewPassword))
            .ReturnsAsync(IdentityResult.Success);
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldChangePassword()
    {
        // Act
        await _handler.Handle(_command, _cancellationToken);

        // Assert
        _userManager.Verify(
            x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), _command.Payload.Password,
                _command.Payload.NewPassword), Times.Once);
    }

    [Test]
    public void Handle_WhenChangePasswordFailed_ThrowsException()
    {
        // Arrange
        _userManager
            .Setup(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), _command.Payload.Password,
                _command.Payload.NewPassword)).ReturnsAsync(IdentityResult.Failed());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => { await _handler.Handle(_command, _cancellationToken); });
    }
}