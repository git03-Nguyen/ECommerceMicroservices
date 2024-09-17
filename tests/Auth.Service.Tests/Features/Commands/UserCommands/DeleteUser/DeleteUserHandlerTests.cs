using Auth.Service.Features.Commands.UserCommands.DeleteUser;
using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MassTransit;

namespace Auth.Service.Tests.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserHandlerTests
{
    private DeleteUserCommand _command;
    private Fixture _fixture;

    private DeleteUserHandler _handler;
    private Mock<IIdentityService> _identityService;
    private Mock<ILogger<DeleteUserHandler>> _logger;
    private Mock<IPublishEndpoint> _publishEndpoint;
    private Mock<UserManager<ApplicationUser>> _userManager;

    [SetUp]
    public void SetUp()
    {
        _identityService = new Mock<IIdentityService>();
        _logger = new Mock<ILogger<DeleteUserHandler>>();
        _userManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        _publishEndpoint = new Mock<IPublishEndpoint>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _handler = new DeleteUserHandler(_userManager.Object, _logger.Object, _identityService.Object,
            _publishEndpoint.Object);
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _command = new DeleteUserCommand(Guid.NewGuid());

        _identityService.Setup(x => x.IsUserAdmin()).Returns(true);
    }

    [Test]
    public void Handle_WhenNotAdminOrResourceOwner_ThrowsUnAuthorizedAccessException()
    {
        // Arrange
        _identityService.Setup(x => x.IsUserAdmin()).Returns(false);
        _identityService.Setup(x => x.IsResourceOwnerById(It.IsAny<string>())).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(async () =>
        {
            await _handler.Handle(_command, new CancellationToken());
        });
    }

    [Test]
    public void Handle_WhenUserNotFound_ThrowsNotFoundException()
    {
        // Arrange
        _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(_command, new CancellationToken());
        });
    }

    [Test]
    public void Handle_WhenUserIsDeleted_ThrowsResourceNotFoundException()
    {
        // Arrange
        var user = _fixture.Create<ApplicationUser>();
        user.IsDeleted = true;
        _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManager.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(_command, new CancellationToken());
        });
    }

    [Test]
    public async Task Handle_AfterUserIsDeleted_ShouldPublishUserDeletedEvent()
    {
        // Arrange
        var user = _fixture.Create<ApplicationUser>();
        user.IsDeleted = false;
        _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManager.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "role" });

        // Act
        await _handler.Handle(_command, new CancellationToken());

        // Assert
        _publishEndpoint.Verify(x => x.Publish<IAccountDeleted>(It.IsAny<object>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}