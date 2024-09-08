using Auth.Service.Features.Queries.UserQueries.GetUserByEmail;
using Auth.Service.Tests.Extensions;

namespace Auth.Service.Tests.Features.Queries.UserQueries.GetUserByEmail;

[TestFixture]
public class GetUserByEmailValidatorTests
{
    private GetUserByEmailValidator _validator;
    private GetUserByEmailRequest _request;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new GetUserByEmailValidator();
        _request = new GetUserByEmailRequest
        {
            Email = "sub.user123@domain.com"
        };
    }

    #region Setup Test Cases

    private static IEnumerable<TestCaseData> InvalidEmailTestCases()
    {
        yield return new TestCaseData(null).SetName("Email is null");
        yield return new TestCaseData(string.Empty).SetName("Email is empty");
        yield return new TestCaseData("sub.user123").SetName("Email missing domain");
        yield return new TestCaseData("sub.user123@.com").SetName("Email missing domain name");
        yield return new TestCaseData("sub.user123@domain").SetName("Email missing top-level domain");
    }
    
    #endregion

    #region Setup Tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var query = new GetUserByEmailQuery(_request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidEmailTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRequest(string invalidEmail)
    {
        // Arrange
        var request = new GetUserByEmailRequest
        {
            Email = invalidEmail
        };
        
        var query = new GetUserByEmailQuery(request);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    

    #endregion
    
}