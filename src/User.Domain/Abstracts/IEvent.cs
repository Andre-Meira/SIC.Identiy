namespace User.Domain.Abstracts;

/// <summary>
///    Interface de implentacao de envio de Eventos
/// </summary>
public interface IEvent
{
    void RaiseDomainEvent(IDomainEvent @event);
    void ClearEvents();
    IReadOnlyCollection<IDomainEvent> GetEvents(); 
}