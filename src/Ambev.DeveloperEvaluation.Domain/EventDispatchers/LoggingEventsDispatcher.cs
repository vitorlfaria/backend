using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.EventDispatchers;

public class LoggingEventsDispatcher : IDomainEventsDispatcher
{
    private readonly ILogger<LoggingEventsDispatcher> _logger;

    public LoggingEventsDispatcher(ILogger<LoggingEventsDispatcher> logger)
    {
        _logger = logger;
    }

    public Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            _logger.LogInformation($"Domain event dispatched: {domainEvent.GetType().Name} at {domainEvent.OccurredOn}");
        }

        return Task.CompletedTask;
    }
}