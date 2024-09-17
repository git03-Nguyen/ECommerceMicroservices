using System.Linq.Expressions;
using Basket.Service.Features.Commands.BasketCommands.CreateBasket;
using Contracts.Exceptions;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.CreateBasket;

[TestFixture]
public class CreateBasketHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<CreateBasketHandler>>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new CreateBasketHandler(_logger.Object, _unitOfWork.Object);
    }

    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<CreateBasketHandler>> _logger;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private CreateBasketHandler _handler;

    [Test]
    public async Task Handle_WhenGivenValidRequest_ShouldCreateBasket()
    {
        // Arrange
        var request = _fixture.Create<CreateBasketCommand>();

        _unitOfWork.Setup(
                u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
            .Returns(new List<Data.Models.Basket>().AsQueryable().BuildMock());

        // Act
        await _handler.Handle(request, _cancellationToken);

        // Assert
        _unitOfWork.Verify(
            u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()), Times.Once);
        _unitOfWork.Verify(u => u.BasketRepository.AddAsync(It.IsAny<Data.Models.Basket>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
        Assert.Pass();
    }

    [Test]
    public async Task Handle_WhenBasketExists_ShouldThrowResourceExistsException()
    {
        // Arrange
        var request = _fixture.Create<CreateBasketCommand>();

        _unitOfWork.Setup(
                u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
            .Returns(new List<Data.Models.Basket> { _fixture.Create<Data.Models.Basket>() }.AsQueryable().BuildMock());

        // Act & Assert
        Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _handler.Handle(request, _cancellationToken));
    }
}