using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.DeleteUser;

/// <summary>
/// Contains unit tests for the DeleteUserHandler class.
/// Tests cover successful deletion, user not found, and validation scenarios.
/// </summary>
public class DeleteUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly DeleteUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new DeleteUserHandler(_userRepository);
    }

    /// <summary>
    /// Tests that a user is successfully deleted when a valid ID is provided.
    /// </summary>
    [Fact(DisplayName = "Given valid user ID When deleting user Then returns success response")]
    public async Task Handle_ValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        _userRepository.DeleteAsync(userId, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _userRepository.Received(1).DeleteAsync(userId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that a KeyNotFoundException is thrown when the user to be deleted is not found.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user ID When deleting user Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        _userRepository.DeleteAsync(userId, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"User with ID {userId} not found");
        await _userRepository.Received(1).DeleteAsync(userId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that a ValidationException is thrown when an empty user ID is provided.
    /// </summary>
    [Fact(DisplayName = "Given empty user ID When deleting user Then throws ValidationException")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.Empty);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
        await _userRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    // Additional tests can be added to cover other scenarios or edge cases if needed.
    // For example, testing with a cancellation token that is already cancelled.
    // Or testing how the handler behaves if the repository throws an unexpected exception.
}