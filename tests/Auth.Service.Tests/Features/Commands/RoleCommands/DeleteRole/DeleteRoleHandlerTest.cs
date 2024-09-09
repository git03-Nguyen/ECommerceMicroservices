using Auth.Service.Features.Commands.RoleCommands.DeleteRole;
using Contracts.Exceptions;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.DeleteRole;

[TestFixture]
public class DeleteRoleHandlerTest
{
    [SetUp]
    public void Setup()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(true);

        _roleManagerMock = new Mock<RoleManager<ApplicationRole>>(
            Mock.Of<IRoleStore<ApplicationRole>>(),
            null, null, null, null);

        _cancellationToken = new CancellationToken();
        _fixture = new Fixture();


        _handler = new DeleteRoleHandler(new Mock<ILogger<DeleteRoleHandler>>().Object, _roleManagerMock.Object,
            _identityServiceMock.Object);
    }

    private Mock<IIdentityService> _identityServiceMock;
    private Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private DeleteRoleHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    [TestCase(ApplicationRoleConstants.Customer)]
    [TestCase(ApplicationRoleConstants.Seller)]
    public async Task DeleteRole_WhenUserIsNotAdmin_ThrowUnAuthorizedAccessException(string role)
    {
        // Arrange
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(async () =>
        {
            await _handler.Handle(new DeleteRoleCommand(new DeleteRoleRequest()), _cancellationToken);
        });
    }

    [Test]
    public async Task DeleteRole_WhenRoleDoesNotExist_ThrowResourceNotFoundException()
    {
        // Arrange
        var role = _fixture.Create<string>();
        _roleManagerMock.Setup(x => x.FindByNameAsync(role)).ReturnsAsync((ApplicationRole)null);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(new DeleteRoleCommand(new DeleteRoleRequest { Name = role }), _cancellationToken);
        });
    }

    [Test]
    public async Task DeleteRole_WhenRoleExists_DeleteRole()
    {
        // Arrange
        var role = _fixture.Create<ApplicationRole>();
        _roleManagerMock.Setup(x => x.FindByNameAsync(role.Name)).ReturnsAsync(role);
        _roleManagerMock.Setup(x => x.DeleteAsync(role)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(new DeleteRoleCommand(new DeleteRoleRequest { Name = role.Name }),
            _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(DeleteRoleResponse.Empty));
    }

    [Test]
    public async Task DeleteRole_WhenRoleDeleteFailed_ThrowException()
    {
        // Arrange
        var role = _fixture.Create<ApplicationRole>();
        _roleManagerMock.Setup(x => x.FindByNameAsync(role.Name)).ReturnsAsync(role);
        _roleManagerMock.Setup(x => x.DeleteAsync(role))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _handler.Handle(new DeleteRoleCommand(new DeleteRoleRequest { Name = role.Name }),
                _cancellationToken);
        });
    }
}