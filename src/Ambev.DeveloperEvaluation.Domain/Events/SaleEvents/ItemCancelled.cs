namespace Ambev.DeveloperEvaluation.Domain.Events.SaleEvents;

public record ItemCancelled(Guid SaleId, Guid ProductId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
