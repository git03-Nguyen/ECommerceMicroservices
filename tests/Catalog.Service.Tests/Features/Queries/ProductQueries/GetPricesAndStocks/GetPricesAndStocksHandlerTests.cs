using System.Linq.Expressions;
using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;
using Catalog.Service.Models.Dtos;
using Catalog.Service.Repositories;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetPricesAndStocks;

[TestFixture]
public class GetPricesAndStocksHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private GetPricesAndStocksHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetPricesAndStocksHandler(_unitOfWork.Object);
    }
    
    // [Test]
    // public async Task Handle_ShouldReturnPricesAndStocks_WhenGivenValidRequest()
    // {
    //     // Arrange
    //     var products = _fixture.CreateMany<Product>();
    //     var productDtos = products.Select(p => new ProductPriceStockDto(p)).ToList();
    //     _unitOfWork.Setup(u => u.ProductRepository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
    //         .Returns(products.AsQueryable().BuildMock());
    //     _unitOfWork.Setup(u => u.ProductRepository.CheckExistsByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>()))
    //         .ReturnsAsync(true);
    //     _unitOfWork.Setup(u => u.ProductRepository.GetPriceAndStock(It.IsAny<int[]>(), _cancellationToken))
    //         .Returns(products.AsQueryable().BuildMock());
    //     
    //     var query = new GetPricesAndStocksQuery(new GetPricesAndStocksRequest()
    //     {
    //         Payload = new[]
    //         {
    //             1, 2, 3, 4, 5
    //         }
    //     });
    //     
    //     // Act
    //     var result = await _handler.Handle(query, _cancellationToken);
    //     
    //     // Assert
    //     Assert.That(result.Payload.Count(), Is.EqualTo(products.Count()));
    // }
    
    
}