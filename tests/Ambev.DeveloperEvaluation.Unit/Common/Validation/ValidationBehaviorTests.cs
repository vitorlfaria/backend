using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Validation;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<TestRequest, TestResponse> _behavior;
    private readonly IEnumerable<IValidator<TestRequest>> _validators;
    private readonly IValidator<TestRequest> _validator1;
    private readonly IValidator<TestRequest> _validator2;
    private readonly RequestHandlerDelegate<TestResponse> _next;
    private readonly CancellationToken _cancellationToken;

    public ValidationBehaviorTests()
    {
        _validator1 = Substitute.For<IValidator<TestRequest>>();
        _validator2 = Substitute.For<IValidator<TestRequest>>();
        _validators = new List<IValidator<TestRequest>> { _validator1, _validator2 };
        _behavior = new ValidationBehavior<TestRequest, TestResponse>(_validators);
        _next = Substitute.For<RequestHandlerDelegate<TestResponse>>();
        _cancellationToken = CancellationToken.None;
    }

    [Fact(DisplayName = "Handle should call next when no validators are present")]
    public async Task Handle_ShouldCallNext_WhenNoValidatorsArePresent()
    {
        // Arrange
        var behavior = new ValidationBehavior<TestRequest, TestResponse>([]);
        var request = new TestRequest();

        // Act
        await behavior.Handle(request, _next, _cancellationToken);

        // Assert
        await _next.Received(1)();
    }

    [Fact(DisplayName = "Handle should call next when validation succeeds")]
    public async Task Handle_ShouldCallNext_WhenValidationSucceeds()
    {
        // Arrange
        var request = new TestRequest();
        _validator1.ValidateAsync(Arg.Any<ValidationContext<TestRequest>>(), _cancellationToken)
            .Returns(Task.FromResult(new ValidationResult()));
        _validator2.ValidateAsync(Arg.Any<ValidationContext<TestRequest>>(), _cancellationToken)
            .Returns(Task.FromResult(new ValidationResult()));

        // Act
        await _behavior.Handle(request, _next, _cancellationToken);

        // Assert
        await _next.Received(1)();
    }

    [Fact(DisplayName = "Handle should throw ValidationException when validation fails")]
    public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
    {
        // Arrange
        var request = new TestRequest();
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Property1", "Error message 1"),
            new ValidationFailure("Property2", "Error message 2")
        };
        var validationResult = new ValidationResult(failures);

        _validator1.ValidateAsync(Arg.Any<ValidationContext<TestRequest>>(), _cancellationToken)
            .Returns(Task.FromResult(validationResult));
        _validator2.ValidateAsync(Arg.Any<ValidationContext<TestRequest>>(), _cancellationToken)
            .Returns(Task.FromResult(new ValidationResult()));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _behavior.Handle(request, _next, _cancellationToken));
        Assert.Equal(failures, exception.Errors);
        await _next.DidNotReceive()();
    }

    // Dummy classes for testing
    public class TestRequest : IRequest<TestResponse> { }
    public class TestResponse { }

    // Helper class to represent ValidationFailure (since it's internal in FluentValidation)
    public class ValidationFailure : FluentValidation.Results.ValidationFailure
    {
        public ValidationFailure(string propertyName, string errorMessage)
            : base(propertyName, errorMessage)
        {
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
    }
}