using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Validation;

public class ValidatorTests
{
    [Fact(DisplayName = "ValidateAsync should return no errors when validation succeeds")]
    public async Task ValidateAsync_ShouldReturnNoErrors_WhenValidationSucceeds()
    {
        // Arrange
        var instance = new TestClass();
        var validator = Substitute.For<IValidator<TestClass>>();
        validator.ValidateAsync(Arg.Any<ValidationContext<TestClass>>())
            .Returns(Task.FromResult(new ValidationResult()));

        // Act
        var errors = await Validator.ValidateAsync(instance);

        // Assert
        Assert.Empty(errors);
    }

    [Fact(DisplayName = "ValidateAsync should return errors when validation fails")]
    public async Task ValidateAsync_ShouldReturnErrors_WhenValidationFails()
    {
        // Arrange
        var instance = new TestClass();
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Property1", "Error message 1"),
            new ValidationFailure("Property2", "Error message 2")
        };
        var validationResult = new ValidationResult(failures);

        var validator = Substitute.For<IValidator<TestClass>>();
        validator.ValidateAsync(Arg.Any<ValidationContext<TestClass>>())
            .Returns(Task.FromResult(validationResult));

        // Act
        var errors = await Validator.ValidateAsync(instance);

        // Assert
        Assert.NotEmpty(errors);
        Assert.Equal(failures.Count, errors.Count());
        // You might want to add more specific assertions about the error details
        // if ValidationErrorDetail has properties you want to check.
    }

    [Fact(DisplayName = "ValidateAsync should throw InvalidOperationException when no validator is found")]
    public async Task ValidateAsync_ShouldThrowInvalidOperationException_WhenNoValidatorIsFound()
    {
        // Arrange
        var instance = new ClassWithoutValidator();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => Validator.ValidateAsync(instance));
    }

    // Dummy classes for testing
    public class TestClass { }
    public class TestClassValidator : AbstractValidator<TestClass> { }

    public class ClassWithoutValidator { }

    // Helper class to represent ValidationFailure
    public class ValidationFailure : FluentValidation.Results.ValidationFailure
    {
        public ValidationFailure(string propertyName, string errorMessage) : base(propertyName, errorMessage) { }
    }
}