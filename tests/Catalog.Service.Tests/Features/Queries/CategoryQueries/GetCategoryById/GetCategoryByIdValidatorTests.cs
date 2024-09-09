using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Queries.CategoryQueries.GetCategoryById;

[TestFixture]
public class GetCategoryByIdValidatorTests
{
    private Fixture _fixture;
    private GetCategoryByIdValidator _validator;
    private GetCategoryByIdQuery _query;
    
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetCategoryByIdValidator();
        _query = new GetCategoryByIdQuery(1);
    }

    #region Setup test cases

    private static IEnumerable<TestCaseData> InvalidCategoryIdTestCases()
    {
        yield return new TestCaseData(0).SetName("CategoryId is 0");
        yield return new TestCaseData(-1).SetName("CategoryId is negative");
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
    [TestCaseSource(nameof(InvalidCategoryIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidCategoryId(int categoryId)
    {
        // Arrange
        _query.CategoryId = categoryId;
        
        // Act
        var result = await _validator.ValidateAsync(_query, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    #endregion
    
    
}