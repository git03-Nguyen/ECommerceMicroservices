using System.Linq.Expressions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Models.Dtos;
using Contracts.Constants;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Tests.Features.Queries.BasketQueries.GetBasketOfACustomerTests;

[TestFixture]
public class GetBasketOfACustomerHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private GetBasketOfACustomerHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetBasketOfACustomerHandler(_unitOfWorkMock.Object, _identityServiceMock.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldReturnBasket()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var basket = _fixture.Create<Data.Models.Basket>();
        var basketList = new List<Data.Models.Basket> { basket };
        var basketsMock = basketList.AsQueryable().BuildMock();
        
        _identityServiceMock.Setup(x => x.EnsureIsCustomer());
        _identityServiceMock.Setup(x => x.GetUserId()).Returns(accountId);
        
        _unitOfWorkMock.Setup(x => x.BasketRepository.GetByCondition(x => x.AccountId == accountId))
            .Returns(basketsMock);
        
        // Act
        var response = await _handler.Handle(new GetBasketOfACustomerQuery(), _cancellationToken);
        
        // Assert
        Assert.That(response, Is.Not.Null);
    }
    
    [Test]
    public void Handle_WhenUserIsNotCustomer_ShouldThrowForbiddenAccessException()
    {
        // Arrange
        _identityServiceMock.Setup(x => x.EnsureIsCustomer())
            .Throws(new ForbiddenAccessException());
        
        // Act
        var action = new Func<Task>(() => _handler.Handle(new GetBasketOfACustomerQuery(), _cancellationToken));
        
        // Assert
        Assert.That(action, Throws.Exception.TypeOf<ForbiddenAccessException>());
    }

}