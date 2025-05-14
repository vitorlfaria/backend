using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users;

/// <summary>
/// Contains unit tests for the UsersController class.
/// Tests cover user creation, retrieval, and deletion scenarios.
/// </summary>
public class UsersControllerTests
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UsersController _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersControllerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UsersControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _controller = new UsersController(_mediator, _mapper);
    }

    /// <summary>
    /// Tests that a valid user creation request returns a created response.
    /// </summary>
    [Fact(DisplayName = "Given valid user data When creating user Then returns created response")]
    public async Task CreateUser_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Username = "TestUser",
            Password = "Password123!",
            Email = "test@example.com",
            Phone = "+5541998752806",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };
        var command = new CreateUserCommand
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,
            Phone = request.Phone,
            Status = request.Status,
            Role = request.Role
        };
        var result = new CreateUserResult { Id = Guid.NewGuid() };
        var response = new CreateUserResponse
        {
            Id = result.Id,
            Name = request.Username,
            Email = request.Email,
            Phone = request.Phone,
            Status = request.Status,
            Role = request.Role
        };

        _mapper.Map<CreateUserCommand>(request).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<CreateUserResponse>(result).Returns(response);

        // Act
        var actionResult = await _controller.CreateUser(request, CancellationToken.None);

        // Assert
        var createdResult = actionResult.Should().BeOfType<CreatedResult>().Subject;
        createdResult.StatusCode.Should().Be(201);
    }

    /// <summary>
    /// Tests that an invalid user creation request returns a bad request response.
    /// </summary>
    [Fact(DisplayName = "Given invalid user data When creating user Then returns bad request")]
    public async Task CreateUser_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateUserRequest(); // Invalid request
        var validator = new CreateUserRequestValidator();
        var validationResult = validator.Validate(request);

        // Act
        var actionResult = await _controller.CreateUser(request, CancellationToken.None);

        // Assert
        var badRequestResult = actionResult.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be(400);
    }

    /// <summary>
    /// Tests that a valid user retrieval request returns a success response with user details.
    /// </summary>
    [Fact(DisplayName = "Given valid user ID When retrieving user Then returns user details")]
    public async Task GetUser_ValidId_ReturnsUserDetails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var result = new GetUserResult
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com",
            Phone = "+1 1234567890",
            Role = UserRole.Customer,
            Status = UserStatus.Active
        };
        var response = new GetUserResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Phone = result.Phone,
            Role = result.Role,
            Status = result.Status
        };

        _mediator.Send(Arg.Is<GetUserCommand>(c => c.Id == userId), Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<GetUserResponse>(result).Returns(response);

        // Act
        var actionResult = await _controller.GetUser(userId, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(200);
    }


    /// <summary>
    /// Tests that a valid user deletion request returns a success response.
    /// </summary>
    [Fact(DisplayName = "Given valid user ID When deleting user Then returns success response")]
    public async Task DeleteUser_ValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        _mapper.Map<DeleteUserCommand>(userId).Returns(command);

        // Act
        var actionResult = await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(200);
    }

    /// <summary>
    /// Tests that a request to delete a non-existent user returns a not found response.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user ID When deleting user Then returns not found")]
    public async Task DeleteUser_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand(userId);

        _mapper.Map<DeleteUserCommand>(userId).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).ThrowsAsync(new KeyNotFoundException($"User with ID {userId} not found"));

        // Act
        var act = async () => await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"User with ID {userId} not found");
    }

    /// <summary>
    /// Tests that the controller handles validation errors for GetUser requests.
    /// </summary>
    [Fact(DisplayName = "GetUser_WithValidationErrors_ReturnsBadRequest")]
    public async Task GetUser_WithValidationErrors_ReturnsBadRequest()
    {
        // Arrange
        var userId = Guid.Empty; // Invalid ID
        var validator = new GetUserRequestValidator();
        var validationResult = validator.Validate(new GetUserRequest { Id = userId });

        // Act
        var actionResult = await _controller.GetUser(userId, CancellationToken.None);

        // Assert
        var badRequestResult = actionResult.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be(400);
    }

    /// <summary>
    /// Tests that the controller handles validation errors for DeleteUser requests.
    /// </summary>
    [Fact(DisplayName = "DeleteUser_WithValidationErrors_ReturnsBadRequest")]
    public async Task DeleteUser_WithValidationErrors_ReturnsBadRequest()
    {
        // Arrange
        var userId = Guid.Empty; // Invalid ID
        var validator = new DeleteUserRequestValidator();
        var validationResult = validator.Validate(new DeleteUserRequest { Id = userId });

        // Act
        var actionResult = await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        var badRequestResult = actionResult.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be(400);
    }
}