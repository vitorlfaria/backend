using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

/// <summary>
/// Contains unit tests for the DefaultContext class.
/// Tests cover database context creation and configuration.
/// </summary>
public class DefaultContextTests
{
    /// <summary>
    /// Tests that the DefaultContext can be created with the provided options.
    /// </summary>
    [Fact(DisplayName = "DefaultContext should be created with provided options")]
    public void Given_DbContextOptions_When_ContextCreated_Then_ShouldNotBeNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Act
        var context = new DefaultContext(options);

        // Assert
        Assert.NotNull(context);
    }

    /// <summary>
    /// Tests that the Users DbSet is correctly defined in the DefaultContext.
    /// </summary>
    [Fact(DisplayName = "DefaultContext should have a Users DbSet")]
    public void Given_DbContext_When_AccessingUsersDbSet_Then_ShouldNotBeNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new DefaultContext(options);

        // Act
        var usersDbSet = context.Users;

        // Assert
        Assert.NotNull(usersDbSet);
        Assert.IsAssignableFrom<DbSet<User>>(usersDbSet);
    }

    /// <summary>
    /// Tests that the DefaultContextFactory can create a DefaultContext instance.
    /// </summary>
    [Fact(DisplayName = "DefaultContextFactory should create a DefaultContext instance")]
    public void Given_DefaultContextFactory_When_CreateDbContextCalled_Then_ShouldReturnDbContextInstance()
    {
        // Arrange
        var factory = new YourDbContextFactory();

        // Act
        var context = factory.CreateDbContext(Array.Empty<string>());

        // Assert
        Assert.NotNull(context);
        Assert.IsType<DefaultContext>(context);
    }
}