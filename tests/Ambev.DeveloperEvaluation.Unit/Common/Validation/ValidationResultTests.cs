using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Validation;

/// <summary>
/// Contains unit tests for the ValidationResultDetail class.
/// Tests cover initialization and property setting.
/// </summary>
public class ValidationResultDetailTests
{
    /// <summary>
    /// Tests that the default constructor initializes the IsValid property to false and Errors to an empty collection.
    /// </summary>
    [Fact(DisplayName = "Default constructor should initialize with IsValid false and empty Errors")]
    public void DefaultConstructor_ShouldInitializeWithDefaults()
    {
        // Act
        var validationResultDetail = new ValidationResultDetail();

        // Assert
        Assert.False(validationResultDetail.IsValid);
        Assert.Empty(validationResultDetail.Errors);
    }

    /// <summary>
    /// Tests that the constructor with ValidationResult parameter correctly initializes properties based on the provided result.
    /// </summary>
    [Fact(DisplayName = "Constructor with ValidationResult should initialize properties correctly")]
    public void ConstructorWithValidationResult_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("PropertyName", "Error message"));

        // Act
        var validationResultDetail = new ValidationResultDetail(validationResult);

        // Assert
        Assert.False(validationResultDetail.IsValid);
        Assert.NotEmpty(validationResultDetail.Errors);
        Assert.Single(validationResultDetail.Errors);
    }

    /// <summary>
    /// Tests that the IsValid property can be set and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "IsValid property should be settable and gettable")]
    public void IsValidProperty_ShouldBeSettableAndGettable()
    {
        // Arrange
        var validationResultDetail = new ValidationResultDetail();

        // Act
        validationResultDetail.IsValid = true;

        // Assert
        Assert.True(validationResultDetail.IsValid);
    }
}