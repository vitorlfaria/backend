using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Common;

/// <summary>
/// Contains unit tests for the BaseController class.
/// Tests cover action results and user context retrieval.
/// </summary>
public class BaseControllerTests
{
    private readonly TestsController _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseControllerTests"/> class.
    /// Sets up a mock BaseController for testing.
    /// </summary>
    public BaseControllerTests()
    {
        _controller = new TestsController();
    }

    /// <summary>
    /// Sets the current user context for the controller.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="email">The user email.</param>
    private void SetUserContext(string userId, string email)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email)
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    /// <summary>
    /// Tests that GetCurrentUserId returns the correct user ID from the context.
    /// </summary>
    [Fact(DisplayName = "GetCurrentUserId should return the correct user ID")]
    public void GetCurrentUserId_ShouldReturnCorrectId()
    {
        // Arrange
        SetUserContext("123", "test@example.com");

        // Act
        var result = _controller.CurrentUserId();

        // Assert
        OkObjectResult returnId = Assert.IsType<OkObjectResult>(result);
        ApiResponseWithData<int> response = Assert.IsType<ApiResponseWithData<int>>(returnId.Value);
        Assert.NotNull(response);
        Assert.Equal(123, response.Data);
    }

    /// <summary>
    /// Tests that GetCurrentUserEmail returns the correct email from the context.
    /// </summary>
    [Fact(DisplayName = "GetCurrentUserEmail should return the correct email")]
    public void GetCurrentUserEmail_ShouldReturnCorrectEmail()
    {
        // Arrange
        SetUserContext("123", "test@example.com");

        // Act
        var email = _controller.CurrentUserEmail();

        // Assert
        OkObjectResult returnEmail = Assert.IsType<OkObjectResult>(email);
        ApiResponseWithData<string> response = Assert.IsType<ApiResponseWithData<string>>(returnEmail.Value);
        Assert.NotNull(response);
        Assert.Equal("test@example.com", response.Data);
    }

    /// <summary>
    /// Tests that Ok returns an OkObjectResult with the correct data and success status.
    /// </summary>
    [Fact(DisplayName = "Ok should return OkObjectResult with correct data")]
    public void Ok_ShouldReturnOkObjectResultWithData()
    {
        // Arrange
        var data = new { Value = "Test" };

        // Act
        var result = _controller.Ok(data) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Tests that Created returns a CreatedAtRouteResult with the correct data and success status.
    /// </summary>
    [Fact(DisplayName = "Created should return CreatedAtRouteResult with correct data")]
    public void Created_ShouldReturnCreatedAtRouteResultWithData()
    {
        // Arrange
        var data = new { Value = "Test" };

        // Act
        var result = _controller.CreatedResponse("GetById", new { id = 1 }, data) as CreatedAtRouteResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Tests that BadRequest returns a BadRequestObjectResult with the correct message and failure status.
    /// </summary>
    [Fact(DisplayName = "BadRequest should return BadRequestObjectResult with correct message")]
    public void BadRequest_ShouldReturnBadRequestObjectResultWithMessage()
    {
        // Arrange
        var message = "Invalid request";

        // Act
        var result = _controller.BadRequest(message) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        ApiResponse response = Assert.IsType<ApiResponse>(result.Value);
        Assert.NotNull(response);
        Assert.Equal(message, response.Message);
    }

    /// <summary>
    /// Tests that NotFound returns a NotFoundObjectResult with the correct message and failure status.
    /// </summary>
    [Fact(DisplayName = "NotFound should return NotFoundObjectResult with correct message")]
    public void NotFound_ShouldReturnNotFoundObjectResultWithMessage()
    {
        // Arrange
        var message = "Resource not found";

        // Act
        var result = _controller.NotFound(message) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
        ApiResponse response = Assert.IsType<ApiResponse>(result.Value);
        Assert.NotNull(response);
        Assert.Equal(message, response.Message);
    }

    /// <summary>
    /// Tests that OkPaginated returns an OkObjectResult with the correct paginated data and success status.
    /// </summary>
    [Fact(DisplayName = "OkPaginated should return OkObjectResult with correct paginated data")]
    public async Task OkPaginated_ShouldReturnOkObjectResultWithPaginatedData()
    {
        // Arrange
        var data = new List<int> { 1, 2, 3 };
        var paginatedList = new PaginatedList<int>(data, 10, 1, 3);

        // Act
        var result = _controller.Paginated(paginatedList) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }
}

public class TestsController : BaseController
{
    public IActionResult CurrentUserId()
    {
        return Ok(GetCurrentUserId());
    }

    public IActionResult CurrentUserEmail()
    {
        return Ok(GetCurrentUserEmail());
    }

    public IActionResult CreatedResponse(string routeName, object routeValues, object data)
    {
        return Created(routeName, routeValues, data);
    }

    public IActionResult Paginated<T>(PaginatedList<T> paginatedList)
    {
        return OkPaginated(paginatedList);
    }

    public IActionResult BadRequest(string message)
    {
        return BadRequestResult(message);
    }

    public IActionResult NotFound(string message = "Resource not found")
    {
        return NotFoundResult(message);
    }
}