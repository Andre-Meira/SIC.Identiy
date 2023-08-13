namespace Identiy.Domain.Abstracts;

public interface INotificationDomain
{
    IReadOnlyCollection<Notification> Notifications { get; }

    public void AddNotification(Notification notification); 

}

public sealed record Notification(string key, string value);
