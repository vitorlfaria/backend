using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Represents the result of an update user operation.
/// </summary>
public class UpdateUserResult
{
    /// <summary>
    /// Gets or sets the ID of the updated user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated status.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the updated role.
    /// </summary>
    public UserRole Role { get; set; }
}