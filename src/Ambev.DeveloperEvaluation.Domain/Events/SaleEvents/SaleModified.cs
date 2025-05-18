namespace Ambev.DeveloperEvaluation.Domain.Events.SaleEvents;

public record SaleModified(Guid SaleId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
