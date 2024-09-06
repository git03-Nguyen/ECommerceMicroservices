using Auth.Service.Exceptions;
using Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

namespace Auth.Service.Tests.Features.Queries.UserQueries;

[TestFixture]
public class GetUserByEmailHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _queryMock = new Mock<GetUserByEmailQuery>(new GetUserByEmailRequest());

        _cancellationToken = new CancellationToken();
        _fixture = new Fixture();

        _handler = new GetUserByEmailHandler(new Mock<ILogger<GetUserByEmailHandler>>().Object,
            _userManagerMock.Object);
    }

    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<GetUserByEmailQuery> _queryMock;
    private GetUserByEmailHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    [Test]
    public async Task GetUserByEmail_WhenUserNotFound_ThrowResourceNotFoundException()
    {
        // Arrange
        var email = "email@email.com";
        _queryMock.Object.Payload.Email = email;

        _userManagerMock.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(_queryMock.Object, _cancellationToken);
        });
    }

    [Test]
    public async Task GetUserByEmail_IfDeletedUser_ThrowResourceNotFoundException()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var user = _fixture.Create<ApplicationUser>();
        user.IsDeleted = true;

        _userManagerMock.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync(user);

        // Act & Assert
        var request = new GetUserByEmailRequest { Email = email };
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(new GetUserByEmailQuery(request), _cancellationToken);
        });
    }

    [Test]
    public async Task GetUserByEmail_WhenUserFound_ReturnUser()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var user = _fixture.Create<ApplicationUser>();
        user.IsDeleted = false;

        _userManagerMock.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync(user);

        // Act
        var request = new GetUserByEmailRequest { Email = email };
        var result = await _handler.Handle(new GetUserByEmailQuery(request), _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Payload, Is.Not.Null);
        Assert.That(result.Payload.Id, Is.EqualTo(user.Id));
    }
}