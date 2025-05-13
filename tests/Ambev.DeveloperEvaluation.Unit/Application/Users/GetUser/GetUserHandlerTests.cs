using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser;

/// <summary>
/// Contains unit tests for the GetUserHandler class.
/// Tests cover successful retrieval, user not found, and validation scenarios.
/// </summary>
public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a user is successfully retrieved when a valid ID is provided.
    /// </summary>
    [Fact(DisplayName = "Given valid user ID When retrieving user Then returns user details")]
    public async Task Handle_ValidId_ReturnsUserDetails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new GetUserCommand(userId);
        var user = new User
        {
            Id = userId,
            Username = "TestUser",
            Email = "test@example.com",
            Phone = "+1234567890",
            Role = UserRole.Customer,
            Status = UserStatus.Active
        };
        var userResult = new GetUserResult
        {
            Id = user.Id,
            Name = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Status = user.Status
        };

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
        _mapper.Map<GetUserResult>(user).Returns(userResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.Name.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        await _userRepository.Received(1).GetByIdAsync(userId, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetUserResult>(user);
    }

    /// <summary>
    /// Tests that a KeyNotFoundException is thrown when the user to be retrieved is not found.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user ID When retrieving user Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new GetUserCommand(userId);

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"User with ID {userId} not found");
        await _userRepository.Received(1).GetByIdAsync(userId, Arg.Any<CancellationToken>());
        _mapper.DidNotReceive().Map<GetUserResult>(Arg.Any<User>());
    }
}