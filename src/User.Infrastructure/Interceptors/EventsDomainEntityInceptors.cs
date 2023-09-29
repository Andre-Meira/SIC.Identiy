using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using User.Domain.Abstracts;

namespace User.Infrastructure.Interceptors;

internal sealed class EventsDomainEntityInceptors : SaveChangesInterceptor
{
    private readonly ILogger<EventsDomainEntityInceptors> _logger;
    private readonly IMediator _mediatorEvent;

    public EventsDomainEntityInceptors(
        ILogger<EventsDomainEntityInceptors> logger, 
        IMediator mediatorEvent)
    {
        _logger = logger;
        _mediatorEvent = mediatorEvent;
    }    

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result, CancellationToken cancellationToken = default)
    {

        if (eventData.Context is null)
            return await base.SavedChangesAsync(eventData, result, cancellationToken);

        var domainEntities = eventData.Context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.GetEvents()?.Count > 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.GetEvents())
            .ToList();

        int value = await base.SavedChangesAsync(eventData, result, cancellationToken);
        await DispatchDomainEvents(domainEvents);
        return value;
    }


    private async Task DispatchDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            try
            {
                await _mediatorEvent.Publish(domainEvent);
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
            }
        }
    }
}
