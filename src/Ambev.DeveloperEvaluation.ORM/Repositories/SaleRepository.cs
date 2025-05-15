using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository(DefaultContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    public Task<Sale?> GetByNumberAsync(int number, CancellationToken cancellationToken = default)
    {
        if (number <= 0)
        {
            throw new ArgumentNullException(nameof(number), "Number cannot be less than or equal to zero");
        }

        return _dbSet.FirstOrDefaultAsync(s => s.Number == number, cancellationToken);
    }

    public override async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be empty");
        }

        return await _dbSet
            .Include(s => s.SaleItems)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
