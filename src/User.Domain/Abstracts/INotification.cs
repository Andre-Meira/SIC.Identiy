namespace User.Domain.Abstracts;

public interface INotificationDomain
{
    IReadOnlyCollection<Notification> Notifications { get; }

    public void AddNotification(Notification notification); 

}

public sealed record Notification(string Key, string Value);
