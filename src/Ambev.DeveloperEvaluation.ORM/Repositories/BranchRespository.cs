using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository for managing branch entities.
/// </summary>
public class BranchRespository(DefaultContext context) : BaseRepository<Branch>(context), IBranchRepository
{
    public Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Branch name cannot be null or empty.");
        }

        return _dbSet.FirstOrDefaultAsync(b => b.Name.Contains(name), cancellationToken);
    }
}
