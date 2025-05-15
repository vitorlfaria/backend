using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository : IBaseRepository<Sale>
{
    /// <summary>
    /// Retrieves a sale by its number
    /// </summary>
    /// <param name="number">The sale number to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when the number is null</exception>
    Task<Sale?> GetByNumberAsync(int number, CancellationToken cancellationToken = default);
}
