using Basket.Service.Features.Commands.BasketCommands.UpdateItem;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.UpdateItem;

[TestFixture]
public class UpdateItemValidatorTests
{
    private UpdateItemValidator _validator;
    private UpdateItemRequest _request;
    private Fixture _fixture;
    
    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateItemValidator();
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _request = new UpdateItemRequest
        {
            BasketId = 1,
            ProductId = 1,
            Quantity = 1
        };
    }

    #region Setup Test cases

    private static IEnumerable<TestCaseData> InvalidBasketIdTestCases()
    {
        yield return new TestCaseData(0).SetName("BasketId is 0");
        yield return new TestCaseData(-1).SetName("BasketId is less than 0");
    }
    
    private static IEnumerable<TestCaseData> InvalidProductIdTestCases()
    {
        yield return new TestCaseData(0).SetName("ProductId is 0");
        yield return new TestCaseData(-1).SetName("ProductId is less than 0");
    }
    
    private static IEnumerable<TestCaseData> InvalidQuantityTestCases()
    {
        yield return new TestCaseData(-1).SetName("Quantity is less than 0");
    }

    #endregion
    
    #region Setup Tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new UpdateItemCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidBasketIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidBasketId(int basketId)
    {
        // Arrange
        _request.BasketId = basketId;
        
        var command = new UpdateItemCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidProductIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidProductId(int productId)
    {
        // Arrange
        _request.ProductId = productId;
        
        var command = new UpdateItemCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidQuantityTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidQuantity(int quantity)
    {
        // Arrange
        _request.Quantity = quantity;
        
        var command = new UpdateItemCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    
    #endregion
    
}