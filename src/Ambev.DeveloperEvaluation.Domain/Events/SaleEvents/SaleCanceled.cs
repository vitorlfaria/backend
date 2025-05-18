namespace Ambev.DeveloperEvaluation.Domain.Events.SaleEvents;

public record SaleCanceled(Guid SaleId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
