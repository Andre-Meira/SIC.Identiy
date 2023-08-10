namespace Identiy.Domain.Abstracts;


public abstract class Entity : IDomainEvent
{
    private readonly List<IEvent> _events = new List<IEvent>();

    public decimal Id { get; private set; }
    public bool IsDeleted { get; private set; }

    public void Remove() => IsDeleted = true;

    public void SetId(decimal id)
    {
        if (Id != default)
            throw new DomainExceptions("Esse registro já possui um id.");       

        Id = id;
    }

    public virtual void Validate() { }

    public void AddEvent(IEvent @event) => _events.Add(@event);

    public void ClearEvents() => _events.Clear();   

    public IReadOnlyCollection<IEvent> GetEvents() => _events;
   
}
