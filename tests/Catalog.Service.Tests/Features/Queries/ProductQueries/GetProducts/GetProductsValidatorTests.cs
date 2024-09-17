using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetProducts;
using Catalog.Service.Models.Filters;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetProducts;

[TestFixture]
public class GetProductsValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetProductsValidator();
        _request = new GetProductsRequest
        {
            CategoryIds = "1",
            MinPrice = 1,
            MaxPrice = 100,
            PageNumber = 1,
            PageSize = 10,
            SortBy = nameof(Product.ProductId),
            SortOrder = FilterConstants.Ascending
        };
    }

    private Fixture _fixture;
    private GetProductsValidator _validator;
    private GetProductsRequest _request;

    private static IEnumerable<TestCaseData> InvalidPageNumberTestCases()
    {
        yield return new TestCaseData(0).SetName("PageNumber is 0");
        yield return new TestCaseData(-1).SetName("PageNumber is negative");
    }

    private static IEnumerable<TestCaseData> InvalidPageSizeTestCases()
    {
        yield return new TestCaseData(0).SetName("PageSize is 0");
        yield return new TestCaseData(-1).SetName("PageSize is negative");
        yield return new TestCaseData(101).SetName("PageSize is greater than 100");
    }

    private static IEnumerable<TestCaseData> InvalidSortByTestCases()
    {
        yield return new TestCaseData(null).SetName("SortBy is null");
        yield return new TestCaseData("InvalidSortBy").SetName("SortBy is invalid");
    }

    private static IEnumerable<TestCaseData> InvalidSortOrderTestCases()
    {
        yield return new TestCaseData(null).SetName("SortOrder is null");
        yield return new TestCaseData("InvalidSortOrder").SetName("SortOrder is invalid");
    }

    private static IEnumerable<TestCaseData> InvalidMinPriceTestCases()
    {
        yield return new TestCaseData((decimal)-1).SetName("MinPrice is negative");
    }

    private static IEnumerable<TestCaseData> InvalidMaxPriceTestCases()
    {
        yield return new TestCaseData((decimal)-1).SetName("MaxPrice is negative");
    }

    private static IEnumerable<TestCaseData> InvalidPriceRangeTestCases()
    {
        yield return new TestCaseData((decimal)100, (decimal)1).SetName("MaxPrice is less than MinPrice");
    }

    private static IEnumerable<TestCaseData> InvalidCategoryIdTestCases()
    {
        yield return new TestCaseData(0).SetName("CategoryId is 0");
        yield return new TestCaseData(-1).SetName("CategoryId is negative");
    }

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidPageNumberTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPageNumber(int pageNumber)
    {
        // Arrange
        _request.PageNumber = pageNumber;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidPageSizeTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPageSize(int pageSize)
    {
        // Arrange
        _request.PageSize = pageSize;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidSortByTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidSortBy(string sortBy)
    {
        // Arrange
        _request.SortBy = sortBy;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidSortOrderTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidSortOrder(string sortOrder)
    {
        // Arrange
        _request.SortOrder = sortOrder;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidMinPriceTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidMinPrice(decimal minPrice)
    {
        // Arrange
        _request.MinPrice = minPrice;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidMaxPriceTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidMaxPrice(decimal maxPrice)
    {
        // Arrange
        _request.MaxPrice = maxPrice;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidPriceRangeTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPriceRange(decimal minPrice, decimal maxPrice)
    {
        // Arrange
        _request.MinPrice = minPrice;
        _request.MaxPrice = maxPrice;
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidCategoryIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidCategoryId(int categoryId)
    {
        // Arrange
        _request.CategoryIds = categoryId.ToString();
        var query = new GetProductsQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
}