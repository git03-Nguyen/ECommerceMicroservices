using Auth.Service.Features.Queries.UserQueries.GetAllUsers;

namespace Auth.Service.Tests.Features.Queries.UserQueries;

[TestFixture]
public class GetAllUsersHandlerTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private GetAllUsersHandler _handler;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    [SetUp]
    public void Setup()
    {
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        
        _cancellationToken = new CancellationToken();
        _fixture = new Fixture();
        
        _handler = new GetAllUsersHandler(_userManagerMock.Object);
    }

    [Test]
    public async Task GetAllUsers_WhenNotSupportQueryableUsers_ThrowNotSupportedException()
    {
        // Arrange
        _userManagerMock.Setup(x => x.SupportsQueryableUsers).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<NotSupportedException>(async () =>
        {
            await _handler.Handle(new GetAllUsersQuery(), _cancellationToken);
        });
    }
    
    [Test]
    public async Task GetAllUsers_WhenSupportQueryableUsers_ReturnAllUsers()
    {
        // Arrange
        _userManagerMock.Setup(x => x.SupportsQueryableUsers).Returns(true);
        
        var users = _fixture.CreateMany<ApplicationUser>().ToList();
        var usersMock = users.AsQueryable().BuildMock();
        _userManagerMock.Setup(x => x.Users).Returns(usersMock);
        
        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), _cancellationToken);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Payload.Count(), Is.EqualTo(users.Count));
    }
}