using Auth.Service.Features.Queries.RoleQueries.GetAllRoles;
using Contracts.Exceptions;

namespace Auth.Service.Tests.Features.Queries.RoleQueries.GetAllRoles;

[TestFixture]
public class GetAllRolesHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _identityServiceMock = new Mock<IIdentityService>();

        _roleManagerMock = new Mock<RoleManager<ApplicationRole>>(
            Mock.Of<IRoleStore<ApplicationRole>>(),
            null, null, null, null);

        _cancellationToken = new CancellationToken();
        _fixture = new Fixture();

        _handler = new GetAllRolesHandler(_roleManagerMock.Object, _identityServiceMock.Object);
    }

    private Mock<IIdentityService> _identityServiceMock;
    private Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private GetAllRolesHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    [Test]
    public async Task GetAllRoles_WhenNotSupportQueryableRoles_ThrowNotSupportedException()
    {
        // Arrange
        _roleManagerMock.Setup(x => x.SupportsQueryableRoles).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<NotSupportedException>(async () =>
        {
            await _handler.Handle(new GetAllRolesQuery(), _cancellationToken);
        });
    }

    [TestCase(ApplicationRoleConstants.Customer)]
    [TestCase(ApplicationRoleConstants.Seller)]
    public async Task GetAllRoles_WhenUserIsNotAdmin_ThrowUnauthorizedAccessException(string role)
    {
        // Arrange
        var userInfo = _fixture.Create<IdentityDto>();
        userInfo.Role = role;

        _roleManagerMock.Setup(x => x.SupportsQueryableRoles).Returns(true);
        _identityServiceMock.Setup(x => x.GetUserInfoIdentity()).Returns(userInfo);

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(async () =>
        {
            await _handler.Handle(new GetAllRolesQuery(), _cancellationToken);
        });
    }

    [Test]
    public async Task GetAllRoles_WhenUserIsAdmin_ReturnRoles()
    {
        // Arrange
        _roleManagerMock.Setup(x => x.SupportsQueryableRoles).Returns(true);
        var roles = _fixture.CreateMany<ApplicationRole>().ToList();
        var rolesMock = roles.AsQueryable().BuildMock();
        _roleManagerMock.Setup(x => x.Roles).Returns(rolesMock);

        var userInfo = _fixture.Create<IdentityDto>();
        userInfo.Role = ApplicationRoleConstants.Admin;
        _identityServiceMock.Setup(x => x.GetUserInfoIdentity()).Returns(userInfo);

        // Act
        var result = await _handler.Handle(new GetAllRolesQuery(), _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Payload.Count(), Is.EqualTo(roles.Count));
    }
}