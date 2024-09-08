using Auth.Service.Features.Queries.UserQueries.GetUserById;
using Auth.Service.Tests.Extensions;

namespace Auth.Service.Tests.Features.Queries.UserQueries.GetUserById;

[TestFixture]
public class GetUserByIdvalidatorTests
{
    private GetUserByIdValidator _validator;
    private Guid _request;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetUserByIdValidator();
        _request = _fixture.Create<Guid>();
    }

    #region Setup Test Cases

    private static IEnumerable<TestCaseData> InvalidIdTestCases()
    {
        yield return new TestCaseData(null).SetName("Id is null");
        yield return new TestCaseData(Guid.Empty).SetName("Id is empty");
        yield return new TestCaseData(Guid.Empty).SetName("Id is not empty");
    }
    
    #endregion

    #region Setup Tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var query = new GetUserByIdQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRequest(Guid invalidId)
    {
        // Arrange
        var request = invalidId;
        var query = new GetUserByIdQuery(request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    

    #endregion 
}