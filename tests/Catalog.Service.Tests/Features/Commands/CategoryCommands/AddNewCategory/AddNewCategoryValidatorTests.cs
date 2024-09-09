using Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.AddNewCategory;

[TestFixture]
public class AddNewCategoryValidatorTests
{
    private Fixture _fixture;
    private AddNewCategoryValidator _validator;
    private AddNewCategoryRequest _request;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new AddNewCategoryValidator();
        _request = new AddNewCategoryRequest()
        {
            Name = "Valid Category",
            Description = "Valid Description",
            ImageUrl = "http://img.jpg"
        };
    }

    #region Setup Test cases

    private static IEnumerable<TestCaseData> InvalidNameTestCases()
    {
        yield return new TestCaseData(string.Empty).SetName("Category name is empty");
        yield return new TestCaseData(null).SetName("Category name is null");
        yield return new TestCaseData(" ").SetName("Category name is whitespace");
        yield return new TestCaseData("a".PadRight(31, 'a')).SetName("Category name is more than 30 characters");
    }
    
    private static IEnumerable<TestCaseData> InvalidDescriptionTestCases()
    {
        yield return new TestCaseData("a".PadRight(501, 'a')).SetName("Category description is more than 500 characters");
    }
    
    private static IEnumerable<TestCaseData> InvalidImageUrlTestCases()
    {
        yield return new TestCaseData(string.Empty).SetName("Category image url is empty");
        yield return new TestCaseData(" ").SetName("Category image url is whitespace");
        yield return new TestCaseData("a").SetName("Category image url is invalid");
        yield return new TestCaseData("http://").SetName("Category image url is invalid");
        yield return new TestCaseData("http://a").SetName("Category image url is invalid");
        yield return new TestCaseData("http://a.mp4").SetName("Category image url is invalid");
    }
    
    #endregion

    #region Setup tests

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new AddNewCategoryCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidName(string name)
    {
        // Arrange
        _request.Name = name;
        var command = new AddNewCategoryCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidDescriptionTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidDescription(string description)
    {
        // Arrange
        _request.Description = description;
        var command = new AddNewCategoryCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidImageUrlTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidImageUrl(string imageUrl)
    {
        // Arrange
        _request.ImageUrl = imageUrl;
        var command = new AddNewCategoryCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    

    #endregion
    
}