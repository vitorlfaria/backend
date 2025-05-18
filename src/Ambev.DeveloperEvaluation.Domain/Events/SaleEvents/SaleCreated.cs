namespace Ambev.DeveloperEvaluation.Domain.Events.SaleEvents;

public record SaleCreated(Guid SaleId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
