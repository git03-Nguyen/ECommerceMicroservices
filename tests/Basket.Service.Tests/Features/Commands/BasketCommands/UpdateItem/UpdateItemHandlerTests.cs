using System.Linq.Expressions;
using System.Net;
using Basket.Service.Features.Commands.BasketCommands.UpdateItem;
using Basket.Service.Models.Dtos;
using Basket.Service.Services;
using Newtonsoft.Json;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.UpdateItem;

[TestFixture]
public class UpdateItemHandlerTests
{
    private Mock<IIdentityService> _identityService;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<UpdateItemHandler>> _logger;
    private Mock<CatalogService> _catalogService;
    private Mock<HttpClient> _httpClient;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private UpdateItemRequest _request;
    private UpdateItemHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _identityService = new Mock<IIdentityService>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<UpdateItemHandler>>();
        _httpClient = new Mock<HttpClient>();
        _catalogService = new Mock<CatalogService>(_httpClient.Object);
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _request = new UpdateItemRequest
        {
            BasketId = 1,
            ProductId = 1,
            Quantity = 1
        };
        
        _handler = new UpdateItemHandler(_logger.Object, _unitOfWork.Object, _identityService.Object, _catalogService.Object);
    }
    
    // [Test]
    // public async Task Handle_WhenGivenValidRequest_ShouldUpdateItem()
    // {
    //     // Arrange
    //     var command = new UpdateItemCommand(_request);
    //     
    //     _unitOfWork.Setup(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
    //         .Returns(new List<Data.Models.Basket> { _fixture.Create<Data.Models.Basket>() }.AsQueryable().BuildMock());
    //
    //     var productId = _request.ProductId;
    //     _httpClient.Setup(c => c.GetAsync($"api/v1/Product/GetById/{productId}", _cancellationToken))
    //         .ReturnsAsync(new HttpResponseMessage
    //         {
    //             StatusCode = HttpStatusCode.OK,
    //             Content = new StringContent(JsonConvert.SerializeObject(_fixture.Create<CatalogService.GetProductsByIdsResponse>()))
    //         });
    //     
    //     // Act
    //     await _handler.Handle(command, _cancellationToken);
    //     
    //     // Assert
    //     _unitOfWork.Verify(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()), Times.Once);
    //     _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    //     Assert.Pass();
    // }
    
    
    
}