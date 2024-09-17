using Auth.Service.Features.Queries.UserQueries.GetOwnProfileQuery;

namespace Auth.Service.Tests.Features.Queries.UserQueries.GetOwnProfile;

public class GetOwnProfileHandlerTests
{
    private CancellationToken _cancellationToken;
    private Fixture _fixture;

    private GetOwnProfileHandler _handler;
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<UserManager<ApplicationUser>> _userManagerMock;

    [SetUp]
    public void Setup()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _fixture = new Fixture();
        _cancellationToken = new CancellationToken();

        _handler = new GetOwnProfileHandler(_identityServiceMock.Object, _userManagerMock.Object);
    }

    [TestCase(ApplicationRoleConstants.Customer)]
    [TestCase(ApplicationRoleConstants.Seller)]
    [TestCase(ApplicationRoleConstants.Admin)]
    public async Task GetOwnProfile_WhenUserIsAdmin_ReturnUserAndRole(string role)
    {
        // Arrange
        var userInfo = _fixture.Create<IdentityDto>();
        userInfo.Role = role;

        var user = _fixture.Create<ApplicationUser>();
        _identityServiceMock.Setup(x => x.GetUserInfoIdentity()).Returns(userInfo);
        _userManagerMock.Setup(x => x.FindByIdAsync(userInfo.Id)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { role });

        // Act
        var result = await _handler.Handle(new GetOwnProfileQuery(), _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Payload, Is.Not.Null);
        Assert.That(result.Payload.Id, Is.EqualTo(user.Id));
        Assert.That(result.Payload.Role, Is.EqualTo(role));
    }
}