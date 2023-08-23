namespace User.Domain.Abstracts;

public abstract class Entity : IEvent, INotificationDomain
{
    readonly List<IDomainEvent> _events;

    readonly List<Notification> _notifications;

    protected Entity()
    {
        _events = new List<IDomainEvent>();
        _notifications = new List<Notification>();        
    }

    public Guid Id { get; private set; }
    public bool IsDeleted { get; private set; }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public virtual void Create()
    {
        Validate();
        Id = Guid.NewGuid();
    }

    public void Remove() => IsDeleted = true;  

    public virtual void Validate() 
    {
        if (_notifications.Any() == true)
            throw new DomainExceptions(_notifications);
    }

    public void RaiseDomainEvent(IDomainEvent @event) => _events.Add(@event);

    public void ClearEvents() => _events.Clear();   

    public IReadOnlyCollection<IDomainEvent> GetEvents() => _events;

    public void AddNotification(Notification notification)
        => _notifications.Add(notification);

    public void AddNotification(List<Notification> notification)
        => _notifications.AddRange(notification);
}
