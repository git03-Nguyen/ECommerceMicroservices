using Basket.Service.Models.Dtos;
using Contracts.Exceptions;

namespace Basket.Service.Tests.Features.Queries.BasketQueries.GetAllBaskets;

[TestFixture]
public class GetAllBasketHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private GetAllBasketsHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetAllBasketsHandler(_identityServiceMock.Object, _unitOfWorkMock.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldReturnGetAllBasketsResponse()
    {
        // Arrange
        var baskets = _fixture.CreateMany<Data.Models.Basket>(3);
        var basketList = baskets.ToList();
        var basketsMock = basketList.AsQueryable().BuildMock();

        _unitOfWorkMock.Setup(x => x.BasketRepository.GetAll()).Returns(basketsMock);
        _identityServiceMock.Setup(x => x.EnsureIsAdmin());
        
        // Act
        var result = await _handler.Handle(new GetAllBasketsQuery(), _cancellationToken);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<GetAllBasketsResponse>());
        Assert.That(result.Payload, Is.Not.Null);
        Assert.That(result.Payload, Is.TypeOf<List<BasketDto>>());
        Assert.That(result.Payload.Count(), Is.EqualTo(basketList.Count));
    }
    
    [Test]
    public void Handle_WhenCalledAndUserIsNotAdmin_ShouldThrowUnAuthorizedAccessException()
    {
        // Arrange
        _identityServiceMock.Setup(x => x.EnsureIsAdmin()).Throws<UnAuthorizedAccessException>();
        
        // Act
        var exception = Assert.ThrowsAsync<UnAuthorizedAccessException>(() => _handler.Handle(new GetAllBasketsQuery(), _cancellationToken));
        
        // Assert
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception, Is.TypeOf<UnAuthorizedAccessException>());
    }
    
}