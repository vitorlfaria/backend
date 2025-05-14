namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Base respository interface for generic entity operations
/// </summary>
public interface IBaseRepository<T> : IDisposable where T : class
{
    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities from the repository paginated
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve</param>
    /// <param name="pageSize">The number of entities per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of entities for the specified page</returns>
    Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
