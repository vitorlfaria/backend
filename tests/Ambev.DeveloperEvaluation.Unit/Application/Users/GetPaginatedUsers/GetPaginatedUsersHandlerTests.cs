using Ambev.DeveloperEvaluation.Application.Users.GetPaginatedUsers;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetPaginatedUsers;

/// <summary>
/// Contains unit tests for the <see cref="GetPaginatedUsersHandler"/> class.
/// </summary>
public class GetPaginatedUsersHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetPaginatedUsersHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaginatedUsersHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetPaginatedUsersHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetPaginatedUsersHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a paginated list of users is retrieved successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid pagination parameters When retrieving users Then returns paginated list")]
    public async Task Handle_ValidRequest_ReturnsPaginatedList()
    {
        // Given
        var pageNumber = 1;
        var pageSize = 10;
        var command = new GetPaginatedUsersCommand { PageNumber = pageNumber, PageSize = pageSize };
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Username = "User1", Email = "user1@example.com", Phone = "+1234567890", Role = UserRole.Customer, Status = UserStatus.Active },
            new User { Id = Guid.NewGuid(), Username = "User2", Email = "user2@example.com", Phone = "+0987654321", Role = UserRole.Admin, Status = UserStatus.Inactive }
        };
        var paginatedUsers = new PaginatedList<User>(users, users.Count, pageNumber, pageSize);
        var results = users.Select(u => new GetPaginatedUsersResult { Id = u.Id, Name = u.Username, Email = u.Email, Phone = u.Phone, Role = u.Role, Status = u.Status }).ToList();

        _userRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedUsers);
        _mapper.Map<GetPaginatedUsersResult>(Arg.Any<User>()).ReturnsForAnyArgs(x => results.First(r => r.Id == ((User)x[0]).Id));

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(users.Count);
        result.CurrentPage.Should().Be(pageNumber);
        result.PageSize.Should().Be(pageSize);
        result.TotalCount.Should().Be(users.Count);
        await _userRepository.Received(1).GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the user repository is called with the correct pagination parameters.
    /// </summary>
    [Fact(DisplayName = "Given pagination parameters When handling Then calls repository with correct parameters")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectParameters()
    {
        // Given
        var pageNumber = 2;
        var pageSize = 5;
        var command = new GetPaginatedUsersCommand { PageNumber = pageNumber, PageSize = pageSize };
        var users = new List<User>();
        var paginatedUsers = new PaginatedList<User>(users, 0, pageNumber, pageSize);

        _userRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedUsers);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _userRepository.Received(1).GetPaginatedAsync(Arg.Is(pageNumber), Arg.Is(pageSize), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called for each user in the paginated list.
    /// </summary>
    [Fact(DisplayName = "Given paginated users When handling Then maps each user to result")]
    public async Task Handle_PaginatedUsers_MapsEachUserToResult()
    {
        // Given
        var pageNumber = 1;
        var pageSize = 10;
        var command = new GetPaginatedUsersCommand { PageNumber = pageNumber, PageSize = pageSize };
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Username = "User1", Email = "user1@example.com", Phone = "+1234567890", Role = UserRole.Customer, Status = UserStatus.Active },
            new User { Id = Guid.NewGuid(), Username = "User2", Email = "user2@example.com", Phone = "+0987654321", Role = UserRole.Admin, Status = UserStatus.Inactive },
            new User { Id = Guid.NewGuid(), Username = "User3", Email = "user3@example.com", Phone = "+1122334455", Role = UserRole.Manager, Status = UserStatus.Active }
        };
        var paginatedUsers = new PaginatedList<User>(users, users.Count, pageNumber, pageSize);
        var results = users.Select(u => new GetPaginatedUsersResult { Id = u.Id, Name = u.Username, Email = u.Email, Phone = u.Phone, Role = u.Role, Status = u.Status }).ToList();

        _userRepository.GetPaginatedAsync(pageNumber, pageSize, Arg.Any<CancellationToken>()).Returns(paginatedUsers);
        _mapper.Map<GetPaginatedUsersResult>(Arg.Any<User>()).ReturnsForAnyArgs(x => results.First(r => r.Id == ((User)x[0]).Id));

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        foreach (var user in users)
        {
            _mapper.Received(1).Map<GetPaginatedUsersResult>(Arg.Is<User>(u => u.Id == user.Id));
        }
    }
}