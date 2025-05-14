using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth;

/// <summary>
/// Contains unit tests for the AuthController class.
/// Tests cover user authentication scenarios.
/// </summary>
public class AuthControllerTests
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly AuthController _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthControllerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public AuthControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _controller = new AuthController(_mediator, _mapper);
    }

    /// <summary>
    /// Tests that a valid authentication request returns a success response with a token.
    /// </summary>
    [Fact(DisplayName = "Given valid credentials When authenticating Then returns success response with token")]
    public async Task AuthenticateUser_ValidCredentials_ReturnsSuccessResponseWithToken()
    {
        // Arrange
        var request = new AuthenticateUserRequest { Email = "test@example.com", Password = "password" };
        var command = new AuthenticateUserCommand { Email = request.Email, Password = request.Password };
        var result = new AuthenticateUserResult { Token = "generatedToken", Email = request.Email, Name = "Test User", Role = "Customer" };
        var response = new AuthenticateUserResponse { Token = result.Token, Email = result.Email, Name = result.Name, Role = result.Role };

        _mapper.Map<AuthenticateUserCommand>(request).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);
        _mapper.Map<AuthenticateUserResponse>(result).Returns(response);

        // Act
        var actionResult = await _controller.AuthenticateUser(request, CancellationToken.None);

        // Assert
        actionResult.Should().BeOfType<OkObjectResult>();
    }

    /// <summary>
    /// Tests that an invalid authentication request returns a bad request response with validation errors.
    /// </summary>
    [Fact(DisplayName = "Given invalid credentials When authenticating Then returns bad request with errors")]
    public async Task AuthenticateUser_InvalidCredentials_ReturnsBadRequestWithErrors()
    {
        // Arrange
        var request = new AuthenticateUserRequest { Email = "", Password = "" }; // Invalid request
        var validator = new AuthenticateUserRequestValidator();
        var validationResult = validator.Validate(request);

        // Act
        var actionResult = await _controller.AuthenticateUser(request, CancellationToken.None);

        // Assert
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    /// <summary>
    /// Tests that a unauthorized exception during authentication returns a 401 Unauthorized response.
    /// </summary>
    [Fact(DisplayName = "Given unauthorized exception When authenticating Then returns unauthorized response")]
    public async Task AuthenticateUser_UnauthorizedAccessException_ReturnsUnauthorizedResponse()
    {
        // Arrange
        var request = new AuthenticateUserRequest { Email = "test@example.com", Password = "wrongpassword" };
        var command = new AuthenticateUserCommand { Email = request.Email, Password = request.Password };

        _mapper.Map<AuthenticateUserCommand>(request).Returns(command);
        _mediator.Send(command, Arg.Any<CancellationToken>()).ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

        // Act
        var act = () => _controller.AuthenticateUser(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
    }

    /// <summary>
    /// Tests that the controller handles validation errors from the request validator.
    /// </summary>
    [Fact(DisplayName = "Given validation errors When authenticating Then returns bad request with validation errors")]
    public async Task AuthenticateUser_WithValidationErrors_ReturnsBadRequestWithValidationErrors()
    {
        // Arrange
        var request = new AuthenticateUserRequest(); // Empty request to trigger validation errors
        var validator = new AuthenticateUserRequestValidator();
        var validationResult = validator.Validate(request);

        // Act
        var actionResult = await _controller.AuthenticateUser(request, CancellationToken.None);

        // Assert
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }
}