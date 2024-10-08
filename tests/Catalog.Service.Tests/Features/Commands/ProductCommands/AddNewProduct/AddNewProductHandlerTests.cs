using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.ProductCommands.AddNewProduct;
using Catalog.Service.Repositories;
using Contracts.Services.Identity;
using MassTransit;

namespace Catalog.Service.Tests.Features.Commands.ProductCommands.AddNewProduct;

[TestFixture]
public class AddNewProductHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<AddNewProductHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _identityService = new Mock<IIdentityService>();
        _publishEndpoint = new Mock<IPublishEndpoint>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new AddNewProductHandler(_unitOfWork.Object, _logger.Object, _identityService.Object,
            _publishEndpoint.Object);
    }

    private Mock<ILogger<AddNewProductHandler>> _logger;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IIdentityService> _identityService;
    private Mock<IPublishEndpoint> _publishEndpoint;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private AddNewProductHandler _handler;

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAddNewProductResponse()
    {
        // Arrange
        var request = _fixture.Create<AddNewProductCommand>();

        _identityService.Setup(x => x.GetUserId()).Returns(_fixture.Create<Guid>());

        _unitOfWork.Setup(u => u.ProductRepository.AddAsync(It.IsAny<Product>())).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    }

    [Test]
    public async Task Handle_WhenFailedToAddProduct_ShouldThrowException()
    {
        // Arrange
        var request = _fixture.Create<AddNewProductCommand>();

        _identityService.Setup(x => x.GetUserId()).Returns(_fixture.Create<Guid>());

        _unitOfWork.Setup(u => u.ProductRepository.AddAsync(It.IsAny<Product>())).ReturnsAsync(false);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, _cancellationToken));
    }
}