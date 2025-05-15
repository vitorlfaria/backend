using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories;

/// <summary>
/// Contains unit tests for the UserRepository class.
/// Tests cover user creation, retrieval, and deletion.
/// </summary>
public class UserRepositoryTests : IClassFixture<DbContextFixture>
{
    private readonly UserRepository _userRepository;
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepositoryTests"/> class.
    /// Sets up an in-memory database context and a UserRepository instance for testing.
    /// </summary>
    public UserRepositoryTests(DbContextFixture fixture)
    {
        _context = fixture.Context;
        _context.Database.EnsureCreated();
        _userRepository = new UserRepository(_context);
    }

    /// <summary>
    /// Tests that a user can be successfully created and retrieved by ID.
    /// </summary>
    [Fact(DisplayName = "Should create and retrieve user by ID")]
    public async Task CreateAndGetUserById_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password" };

        // Act
        var createdUser = await _userRepository.CreateAsync(user);
        var retrievedUser = await _userRepository.GetByIdAsync(createdUser.Id);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Username, retrievedUser.Username);
        Assert.Equal(user.Email, retrievedUser.Email);
    }

    /// <summary>
    /// Tests that a user can be successfully created and retrieved by email.
    /// </summary>
    [Fact(DisplayName = "Should create and retrieve user by email")]
    public async Task CreateAndGetUserByEmail_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password" };

        // Act
        await _userRepository.CreateAsync(user);
        var retrievedUser = await _userRepository.GetByEmailAsync(user.Email);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Username, retrievedUser.Username);
        Assert.Equal(user.Email, retrievedUser.Email);
    }

    /// <summary>
    /// Tests that retrieving a user by ID returns null when the user does not exist.
    /// </summary>
    [Fact(DisplayName = "Should return null when user ID does not exist")]
    public async Task GetUserById_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var retrievedUser = await _userRepository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedUser);
    }

    /// <summary>
    /// Tests that retrieving a user by email returns null when the user does not exist.
    /// </summary>
    [Fact(DisplayName = "Should return null when user email does not exist")]
    public async Task GetUserByEmail_NonExistingEmail_ShouldReturnNull()
    {
        // Arrange
        var nonExistingEmail = "nonexisting@example.com";

        // Act
        var retrievedUser = await _userRepository.GetByEmailAsync(nonExistingEmail);

        // Assert
        Assert.Null(retrievedUser);
    }

    /// <summary>
    /// Tests that a user can be successfully deleted by ID.
    /// </summary>
    [Fact(DisplayName = "Should delete user by ID")]
    public async Task DeleteUser_ExistingId_ShouldReturnTrue()
    {
        // Arrange
        var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password" };
        var createdUser = await _userRepository.CreateAsync(user);

        // Act
        var deleteResult = await _userRepository.DeleteAsync(createdUser.Id);
        var retrievedUser = await _userRepository.GetByIdAsync(createdUser.Id);

        // Assert
        Assert.True(deleteResult);
        Assert.Null(retrievedUser);
    }

    /// <summary>
    /// Tests that deleting a user by ID returns false when the user does not exist.
    /// </summary>
    [Fact(DisplayName = "Should return false when deleting non-existing user")]
    public async Task DeleteUser_NonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var deleteResult = await _userRepository.DeleteAsync(nonExistingId);

        // Assert
        Assert.False(deleteResult);
    }

    /// <summary>
    /// Tests that the repository interacts with the DbContext as expected during user creation.
    /// </summary>
    [Fact(DisplayName = "CreateAsync should interact with DbContext correctly")]
    public async Task CreateAsync_ShouldInteractWithDbContext()
    {
        // Arrange
        var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password" };

        // Act
        var createdUser = await _userRepository.CreateAsync(user);

        // Assert
        var retrievedUser = await _context.Users.FindAsync(createdUser.Id);
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Username, retrievedUser.Username);
        Assert.Equal(user.Email, retrievedUser.Email);
    }

    /// <summary>
    /// Tests that the repository interacts with the DbContext as expected during user deletion.
    /// </summary>
    [Fact(DisplayName = "DeleteAsync should interact with DbContext correctly")]
    public async Task DeleteAsync_ShouldInteractWithDbContext()
    {
        // Arrange
        var user = new User { Username = "TestUser", Email = "test@example.com", Password = "password" };
        var createdUser = await _userRepository.CreateAsync(user);

        // Act
        await _userRepository.DeleteAsync(createdUser.Id);

        // Assert
        var retrievedUser = await _context.Users.FindAsync(createdUser.Id);
        Assert.Null(retrievedUser);
    }
}