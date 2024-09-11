using Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.UpdateCategory;

[TestFixture]
public class UpdateCategoryValidatorTests
{
    private Fixture _fixture;
    private UpdateCategoryValidator _validator;
    private UpdateCategoryCommand _command;
    
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new UpdateCategoryValidator();
        _command = new UpdateCategoryCommand(new UpdateCategoryRequest()
        {
            CategoryId = 1,
            Name = "Valid Name",
            Description = "Valid Description",
            ImageUrl = "http://valid.com/valid.jpg"
        });
    }
    
    
    #region Setup test cases
    
    private static IEnumerable<TestCaseData> InvalidCategoryIdTestCases()
    {
        yield return new TestCaseData(0).SetName("CategoryId is 0");
        yield return new TestCaseData(null).SetName("CategoryId is null");
        yield return new TestCaseData(-1).SetName("CategoryId is negative");
    }
    
    private static IEnumerable<TestCaseData> InvalidNameTestCases()
    {
        yield return new TestCaseData(null).SetName("Name is null");
        yield return new TestCaseData("").SetName("Name is empty");
        yield return new TestCaseData("a".PadRight(101, 'a')).SetName("Name is greater than 100 characters");
    }
    
    private static IEnumerable<TestCaseData> InvalidDescriptionTestCases()
    {
        yield return new TestCaseData("a".PadRight(501, 'a')).SetName("Description is greater than 500 characters");
    }
    
    private static IEnumerable<TestCaseData> InvalidImageUrlTestCases()
    {
        yield return new TestCaseData(null).SetName("ImageUrl is null");
        yield return new TestCaseData("").SetName("ImageUrl is empty");
        yield return new TestCaseData("invalid").SetName("ImageUrl is not a valid URL");
        yield return new TestCaseData("http://").SetName("ImageUrl is not a valid URL");
        yield return new TestCaseData("http://invalid").SetName("ImageUrl is not a valid URL");
        yield return new TestCaseData("http://invalid.com").SetName("ImageUrl is not a valid URL");
    }
    
    #endregion
    
    #region Setup tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidCategoryIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidCategoryId(int categoryId)
    {
        // Arrange
        _command.Payload.CategoryId = categoryId;
        
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidName(string name)
    {
        // Arrange
        _command.Payload.Name = name;
        
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidDescriptionTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidDescription(string description)
    {
        // Arrange
        _command.Payload.Description = description;
        
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidImageUrlTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidImageUrl(string imageUrl)
    {
        // Arrange
        _command.Payload.ImageUrl = imageUrl;
        
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);
        
        // Assert
        Assert.That(result.IsValid, Is.False);
    }
    
    #endregion
    
}