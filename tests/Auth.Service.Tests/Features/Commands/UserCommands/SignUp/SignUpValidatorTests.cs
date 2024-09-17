using Auth.Service.Features.Commands.UserCommands.SignUp;

namespace Auth.Service.Tests.Features.Commands.UserCommands.SignUp;

[TestFixture]
public class SignUpValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new SignUpValidator();
        _request = new SignUpRequest
        {
            UserName = "username",
            Email = "sub.user@domain.com",
            Password = "Admin@123",
            Role = ApplicationRoleConstants.Admin,
            FullName = "Full Name",
            PhoneNumber = "0987654321",
            Address = "Ho Chi Minh City"
        };
    }

    private SignUpValidator _validator;
    private SignUpRequest _request;
    private Fixture _fixture;

    private static IEnumerable<TestCaseData> InvalidUserNameTestCases()
    {
        yield return new TestCaseData(null).SetName("UserName is null");
        yield return new TestCaseData(string.Empty).SetName("UserName is empty");
        yield return new TestCaseData("a b").SetName("UserName contains whitespace");
        yield return new TestCaseData(" ").SetName("UserName is whitespace");
    }

    private static IEnumerable<TestCaseData> InvalidEmailTestCases()
    {
        yield return new TestCaseData(null).SetName("Email is null");
        yield return new TestCaseData(string.Empty).SetName("Email is empty");
        yield return new TestCaseData(" ").SetName("Email is whitespace");
        yield return new TestCaseData("email").SetName("Email is invalid");
        yield return new TestCaseData("email@").SetName("Email is invalid");
        yield return new TestCaseData("email@domain").SetName("Email is invalid");
        yield return new TestCaseData("email@domain.").SetName("Email is invalid");
        yield return new TestCaseData("email@domain.c").SetName("Email is invalid");
        yield return new TestCaseData("email @domain.com").SetName("Email is invalid");
    }

    private static IEnumerable<TestCaseData> InvalidPasswordTestCases()
    {
        yield return new TestCaseData(null).SetName("Password is null");
        yield return new TestCaseData(string.Empty).SetName("Password is empty");
        yield return new TestCaseData(" ").SetName("Password is whitespace");
        yield return new TestCaseData("a").SetName("Password is less than 6 characters");
        yield return new TestCaseData("pass word").SetName("Password contains whitespace");
        yield return new TestCaseData("password").SetName("Password does not contain uppercase letter");
        yield return new TestCaseData("PASSWORD").SetName("Password does not contain lowercase letter");
        yield return new TestCaseData("Password").SetName("Password does not contain number");
        yield return new TestCaseData("Password1").SetName("Password does not contain special character");
    }

    private static IEnumerable<TestCaseData> InvalidRoleTestCases()
    {
        yield return new TestCaseData(null).SetName("Role is null");
        yield return new TestCaseData(string.Empty).SetName("Role is empty");
        yield return new TestCaseData(" ").SetName("Role is whitespace");
        yield return new TestCaseData("role").SetName("Role is invalid");
    }

    private static IEnumerable<TestCaseData> InvalidFullNameTestCases(string role)
    {
        if (role == ApplicationRoleConstants.Admin) yield break;
        yield return new TestCaseData(null).SetName("Full name is null");
        yield return new TestCaseData(string.Empty).SetName("Full name is empty");
        yield return new TestCaseData(" ").SetName("Full name is whitespace");
    }

    private static IEnumerable<TestCaseData> InvalidPhoneNumberTestCases(string role)
    {
        if (role == ApplicationRoleConstants.Admin) yield break;
        yield return new TestCaseData(null).SetName("Phone number is null");
        yield return new TestCaseData(string.Empty).SetName("Phone number is empty");
        yield return new TestCaseData(" ").SetName("Phone number is whitespace");
        yield return new TestCaseData("phone").SetName("Phone number contains non-numeric characters");
    }

    private static IEnumerable<TestCaseData> InvalidAddressTestCases(string role)
    {
        if (role == ApplicationRoleConstants.Admin) yield break;
        yield return new TestCaseData(null).SetName("Address is null");
        yield return new TestCaseData(string.Empty).SetName("Address is empty");
        yield return new TestCaseData(" ").SetName("Address is whitespace");
    }

    [TestCase(ApplicationRoleConstants.Admin)]
    [TestCase(ApplicationRoleConstants.Customer)]
    [TestCase(ApplicationRoleConstants.Seller)]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest(string role)
    {
        // Arrange
        _request.Role = role;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidUserNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRequest(string userName)
    {
        // Arrange
        _request.UserName = userName;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidEmailTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidEmail(string email)
    {
        // Arrange
        _request.Email = email;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidPasswordTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPassword(string password)
    {
        // Arrange
        _request.Password = password;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidRoleTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRole(string role)
    {
        // Arrange
        _request.Role = role;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidFullNameTestCases), new object[] { ApplicationRoleConstants.Admin })]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidFullName(string fullName)
    {
        // Arrange
        _request.FullName = fullName;
        var command = new SignUpCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
}