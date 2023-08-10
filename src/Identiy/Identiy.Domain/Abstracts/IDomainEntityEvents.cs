namespace Identiy.Domain.Abstracts;

/// <summary>
/// 
/// </summary>
public interface IDomainEvent
{
    void AddEvent(IEvent @event);
    void ClearEvents();
    public IReadOnlyCollection<IEvent> GetEvents(); 
}



