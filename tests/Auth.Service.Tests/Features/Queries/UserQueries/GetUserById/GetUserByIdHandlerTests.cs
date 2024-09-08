using Auth.Service.Exceptions;
using Auth.Service.Features.Queries.UserQueries.GetUserByEmail;
using Auth.Service.Features.Queries.UserQueries.GetUserById;
using Auth.Service.Tests.Extensions;

namespace Auth.Service.Tests.Features.Queries.UserQueries.GetUserById;

[TestFixture]
public class GetUserByIdHandlerTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<GetUserByIdQuery> _queryMock;
    private Mock<IIdentityService> _identityServiceMock;
    private GetUserByIdHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    [SetUp]
    public void Setup()
    {
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _fixture = new Fixture().OmitOnRecursionBehavior();
        
        var id = _fixture.Create<Guid>();
        _queryMock = new Mock<GetUserByIdQuery>(id);
        
        _identityServiceMock = new Mock<IIdentityService>();
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(true);
        _identityServiceMock.Setup(x => x.IsResourceOwnerById(_queryMock.Object.Id.ToString())).Returns(true);

        _cancellationToken = new CancellationToken();

        _handler = new GetUserByIdHandler(_identityServiceMock.Object, _userManagerMock.Object);
        
    }
    
    [Test]
    public async Task GetUserById_WhenUserIsNotAdminAndNotResourceOwner_ThrowUnAuthorizedAccessException()
    {
        // Arrange
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(false);
        _identityServiceMock.Setup(x => x.IsResourceOwnerById(_queryMock.Object.Id.ToString())).Returns(false);

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(async () =>
        {
            await _handler.Handle(_queryMock.Object, _cancellationToken);
        });
    }
    
    [Test]
    public async Task GetUserById_WhenUserNotFound_ThrowResourceNotFoundException()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByIdAsync(_queryMock.Object.Id.ToString()))
            .ReturnsAsync((ApplicationUser?)null);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            await _handler.Handle(_queryMock.Object, _cancellationToken);
        });
    }
    
    [Test]
    public async Task GetUserById_WhenUserFound_ReturnUser()
    {
        // Arrange
        var user = _fixture.Create<ApplicationUser>();
        user.IsDeleted = false;

        _userManagerMock.Setup(x => x.FindByIdAsync(_queryMock.Object.Id.ToString()))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Customer" });
        
        // Act
        var result = await _handler.Handle(_queryMock.Object, _cancellationToken);
        
        // Assert
        Assert.That(result.Payload, Is.Not.Null);
        Assert.That(result.Payload.Id, Is.EqualTo(user.Id));
    }
    
}