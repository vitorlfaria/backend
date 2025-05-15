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
}
