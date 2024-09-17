using Auth.Service.Features.Commands.RoleCommands.AddNewRole;
using Contracts.Exceptions;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.AddNewRole;

[TestFixture]
public class AddNewRoleHandlerTests
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
        _fixture = new Fixture().OmitOnRecursionBehavior();

        _handler = new AddNewRoleHandler(new Mock<ILogger<AddNewRoleHandler>>().Object, _roleManagerMock.Object,
            _identityServiceMock.Object);
    }

    private Mock<IIdentityService> _identityServiceMock;
    private Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private AddNewRoleHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    [TestCase(ApplicationRoleConstants.Customer)]
    [TestCase(ApplicationRoleConstants.Seller)]
    public async Task AddNewRole_WhenUserIsNotAdmin_ThrowUnAuthorizedAccessException(string role)
    {
        // Arrange
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(async () =>
        {
            await _handler.Handle(new AddNewRoleCommand(new AddNewRoleRequest()), _cancellationToken);
        });
    }

    [Test]
    public async Task AddNewRole_WhenRoleAlreadyExists_ThrowResourceAlreadyExistsException()
    {
        // Arrange
        var role = _fixture.Create<string>();
        _roleManagerMock.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(true);

        // Act & Assert
        Assert.ThrowsAsync<ResourceAlreadyExistsException>(async () =>
        {
            await _handler.Handle(new AddNewRoleCommand(new AddNewRoleRequest { Name = role }), _cancellationToken);
        });
    }

    [Test]
    public async Task AddNewRole_WhenRoleCreated_ReturnAddNewRoleResponse()
    {
        // Arrange
        var role = _fixture.Create<string>();
        _roleManagerMock.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(false);
        _roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(new AddNewRoleCommand(new AddNewRoleRequest { Name = role }),
            _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(role));
    }

    [Test]
    public async Task AddNewRole_WhenRoleNotCreated_ThrowException()
    {
        // Arrange
        var role = _fixture.Create<string>();
        _roleManagerMock.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(false);
        _roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationRole>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _handler.Handle(new AddNewRoleCommand(new AddNewRoleRequest { Name = role }), _cancellationToken);
        });
    }
}