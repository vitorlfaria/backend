namespace Ambev.DeveloperEvaluation.Domain.Events;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events);
}
