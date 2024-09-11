using System.Linq.Expressions;
using Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;
using Basket.Service.Services;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.CheckoutBasket;

[TestFixture]
public class CheckoutBasketHandlerTests
{
    private Mock<ILogger<CheckoutBasketHandler>> _logger;
    private Mock<IIdentityService> _identityService;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ISendEndpointCustomProvider> _sendEndpointCustomProvider;
    private Mock<CatalogService> _catalogService;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private CheckoutBasketRequest _request;
    private CheckoutBasketHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _logger = new Mock<ILogger<CheckoutBasketHandler>>();
        _identityService = new Mock<IIdentityService>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _sendEndpointCustomProvider = new Mock<ISendEndpointCustomProvider>();
        var httpClient = new Mock<HttpClient>();
        _catalogService = new Mock<CatalogService>(httpClient.Object);
        
        _request = new CheckoutBasketRequest
        {
            BasketId = 1,
            RecipientName = "Nguyen Van A",
            ShippingAddress = "Ho Chi Minh City",
            RecipientPhone = "0123456789"
        };
        
        _handler = new CheckoutBasketHandler(_logger.Object, _identityService.Object, _unitOfWork.Object, _sendEndpointCustomProvider.Object, _catalogService.Object);
        _cancellationToken = new CancellationToken();
    }
    
    // [Test]
    // public async Task Handle_ShouldThrowResourceNotFoundException_WhenBasketDoesNotExist()
    // {
    //     // Arrange
    //     var request = new CheckoutBasketCommand(_request);
    //     var baskets = new Mock<IQueryable<Data.Models.Basket>>();
    //     _unitOfWork.Setup(x => x.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
    //         .Returns(baskets.Object);
    //     baskets.Setup(x => x.FirstOrDefaultAsync(_cancellationToken)).ReturnsAsync((Data.Models.Basket)null);
    //     
    //     // Act
    //     Func<Task> act = async () => await _handler.Handle(request, _cancellationToken);
    //     
    //     // Assert
    //     Assert.That(act, Throws.Exception.TypeOf<ResourceNotFoundException>());
    // }
    
    //TODO: continue to write tests
    
}