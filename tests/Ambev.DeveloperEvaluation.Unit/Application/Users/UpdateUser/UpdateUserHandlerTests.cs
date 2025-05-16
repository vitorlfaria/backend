using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.UpdateUser;

/// <summary>
/// Contains unit tests for the <see cref="UpdateUserHandler"/> class.
/// </summary>
public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UpdateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateUserHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid user update request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid user data When updating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var userId = Guid.NewGuid();
        var command = new UpdateUserCommand
        {
            Id = userId,
            Username = "UpdatedUser",
            Phone = "+19876543210",
            Email = "updated@email.com",
            Status = UserStatus.Inactive,
            Role = UserRole.Admin
        };
        var user = new User { Id = userId, Username = "OriginalUser", Phone = "+1234567890", Email = "original@email.com", Status = UserStatus.Active, Role = UserRole.Customer };
        var result = new UpdateUserResult { Id = userId, Username = command.Username, Phone = command.Phone, Email = command.Email, Status = command.Status, Role = command.Role };

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
        _mapper.Map(command, user);
        _mapper.Map<UpdateUserResult>(user).Returns(result);

        // When
        var updateUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateUserResult.Should().NotBeNull();
        updateUserResult.Id.Should().Be(userId);
        updateUserResult.Username.Should().Be(command.Username);
        updateUserResult.Phone.Should().Be(command.Phone);
        updateUserResult.Email.Should().Be(command.Email);
        updateUserResult.Status.Should().Be(command.Status);
        updateUserResult.Role.Should().Be(command.Role);
        await _userRepository.Received(1).UpdateAsync(Arg.Is(user), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid user update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid user data When updating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateUserCommand(); // Invalid: empty command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that a request to update a non-existent user throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user ID When updating user Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentUser_ThrowsKeyNotFoundException()
    {
        // Given
        var userId = Guid.NewGuid();
        var command = new UpdateUserCommand
        {
            Id = userId,
            Username = "UpdatedUser",
            Phone = "+19876543210",
            Email = "updated@email.com",
            Status = UserStatus.Inactive,
            Role = UserRole.Admin
        };

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID {userId} not found");
        await _userRepository.Received(1).GetByIdAsync(userId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called to map the command to the existing user.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to existing user")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var userId = Guid.NewGuid();
        var command = new UpdateUserCommand
        {
            Id = userId,
            Username = "UpdatedUser",
            Phone = "+19876543210",
            Email = "updated@email.com",
            Status = UserStatus.Inactive,
            Role = UserRole.Admin
        };
        var user = new User
        {
            Id = userId,
            Username = "OriginalUser",
            Phone = "+1234567890",
            Email = "original@email.com",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map(Arg.Is(command), Arg.Is(user));
    }
}