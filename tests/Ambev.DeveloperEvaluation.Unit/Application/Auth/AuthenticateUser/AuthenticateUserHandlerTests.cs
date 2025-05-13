using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

/// <summary>
/// Contains unit tests for the AuthenticateUserHandler class.
/// Tests cover successful authentication, invalid credentials, and inactive user scenarios.
/// </summary>
public class AuthenticateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AuthenticateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public AuthenticateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
    }

    /// <summary>
    /// Tests that a valid user with correct credentials can authenticate successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid credentials When authenticating Then returns success result")]
    public async Task Handle_ValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "test@example.com", Password = "password" };
        var user = UserTestData.GenerateValidUser();
        user.Email = command.Email;
        user.Password = "hashedPassword";
        user.Activate();

        var token = "generatedToken";

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _jwtTokenGenerator.GenerateToken(user).Returns(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(token);
        result.Email.Should().Be(user.Email);
        result.Name.Should().Be(user.Username);
        result.Role.Should().Be(user.Role.ToString());
    }

    /// <summary>
    /// Tests that an exception is thrown when invalid credentials are provided.
    /// </summary>
    [Fact(DisplayName = "Given invalid credentials When authenticating Then throws UnauthorizedAccessException")]
    public async Task Handle_InvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "test@example.com", Password = "wrongPassword" };
        var user = UserTestData.GenerateValidUser();
        user.Email = command.Email;
        user.Password = "hashedPassword";

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(false);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
    }

    /// <summary>
    /// Tests that an exception is thrown when the user is not found.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user When authenticating Then throws UnauthorizedAccessException")]
    public async Task Handle_NonExistentUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "nonexistent@example.com", Password = "password" };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
    }

    /// <summary>
    /// Tests that an exception is thrown when the user is not active.
    /// </summary>
    [Fact(DisplayName = "Given inactive user When authenticating Then throws UnauthorizedAccessException")]
    public async Task Handle_InactiveUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "inactive@example.com", Password = "password" };
        var user = UserTestData.GenerateValidUser();
        user.Email = command.Email;
        user.Password = "hashedPassword";
        user.Status = UserStatus.Inactive;

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("User is not active");
    }

    /// <summary>
    /// Tests that the JWT token generator is called with the correct user.
    /// </summary>
    [Fact(DisplayName = "Given valid credentials When authenticating Then calls JWT token generator with user")]
    public async Task Handle_ValidCredentials_CallsJwtTokenGeneratorWithUser()
    {
        // Arrange
        var command = new AuthenticateUserCommand { Email = "test@example.com", Password = "password" };
        var user = UserTestData.GenerateValidUser();
        user.Email = command.Email;
        user.Password = "hashedPassword";
        user.Activate();

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _jwtTokenGenerator.Received(1).GenerateToken(Arg.Is<User>(u => u.Email == user.Email));
    }
}