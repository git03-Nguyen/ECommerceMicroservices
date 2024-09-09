using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetProductById;

[TestFixture]
public class GetProductByIdValidatorTests
{
    private Fixture _fixture;
    private GetProductByIdValidator _validator;
    private GetProductByIdQuery _query;
    
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetProductByIdValidator();
        _query = new GetProductByIdQuery(1);
    }
    
    #region Setup test cases
    
    private static IEnumerable<TestCaseData> InvalidProductIdTestCases()
    {
        yield return new TestCaseData(0).SetName("ProductId is 0");
        yield return new TestCaseData(-1).SetName("ProductId is negative");
        yield return new TestCaseData(null).SetName("ProductId is null");
    }
    
    #endregion
    
    #region Setup tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        // Act
        var result = await _validator.ValidateAsync(_query, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.True);
    }
    
    [Test]
    [TestCaseSource(nameof(InvalidProductIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidProductId(int productId)
    {
        // Arrange
        _query.ProductId = productId;
        
        // Act
        var result = await _validator.ValidateAsync(_query, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    #endregion
}