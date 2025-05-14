using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Base repository implementation for generic entity operations
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when the id is null</exception>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be empty");
        }
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    /// <summary>
    /// Deletes an entity from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    /// <exception cref="ArgumentNullException">Thrown when the entity is null</exception>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be empty");
        }

        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves all entities from the repository paginated
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve</param>
    /// <param name="pageSize">The number of entities per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of entities for the specified page</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when page number or size is less than 1</exception>
    public async Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0");
        }

        return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Disposes the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
