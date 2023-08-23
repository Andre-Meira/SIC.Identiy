namespace User.Domain.Abstracts;

public abstract record ValueObject : INotificationDomain
{
    private readonly List<Notification> _notifications = new List<Notification>();   
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public abstract void Validate();
}
