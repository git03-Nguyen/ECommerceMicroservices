using Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetManyProducts;

[TestFixture]
public class GetManyProductsValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetManyProductsValidator();
        _query = new GetManyProductsQuery(new GetManyProductsRequest
        {
            Payload = new[]
            {
                1, 2, 3, 4, 5
            }
        });
    }

    private Fixture _fixture;
    private GetManyProductsValidator _validator;
    private GetManyProductsQuery _query;

    private static IEnumerable<TestCaseData> InvalidProductIdsTestCases()
    {
        yield return new TestCaseData(null).SetName("Payload is null");
        yield return new TestCaseData(new int[0]).SetName("Payload is empty");
        yield return new TestCaseData(new[] { 1, 2, 0 }).SetName("Payload contains 0");
        yield return new TestCaseData(new[] { 1, 2, -1 }).SetName("Payload contains negative number");
    }

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
    [TestCaseSource(nameof(InvalidProductIdsTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidProductIds(IEnumerable<int> productIds)
    {
        // Arrange
        _query.Payload.Payload = productIds;

        // Act
        var result = await _validator.ValidateAsync(_query, CancellationToken.None);

        // Assert
        Assert.That(result.IsValid, Is.False);
    }
}