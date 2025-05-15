using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository for managing product entities.
/// </summary>
public class ProductRepository(DefaultContext context) : BaseRepository<Product>(context), IProductRepository
{
    public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Product name cannot be null or empty.");
        }

        return _dbSet.FirstOrDefaultAsync(p => p.Name.Contains(name), cancellationToken);
    }
}
