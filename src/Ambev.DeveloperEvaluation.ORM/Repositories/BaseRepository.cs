using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Base repository implementation for generic entity operations
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be empty");
        }
        return await _dbSet.FindAsync([id], cancellationToken);
    }

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

    public async Task<List<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
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

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }

        _dbSet.Update(entity);
        context.SaveChanges();
        return Task.FromResult(entity);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
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
