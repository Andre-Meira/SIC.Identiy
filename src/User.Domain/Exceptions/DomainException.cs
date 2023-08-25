using Newtonsoft.Json;

namespace User.Domain.Abstracts;

public class DomainExceptions : Exception
{
    public List<Notification>? Messages { get; }

    public DomainExceptions() { }    

    public DomainExceptions(string message) : base(message) { }    

    public DomainExceptions(string message, Exception innerException) 
        : base(message, innerException) { }
    
    public DomainExceptions(List<Notification> messages) 
        : base(ParseString(messages)) => Messages = messages;

    
    public string ToJson() => JsonConvert.SerializeObject(Messages, Formatting.Indented);

    private static string ParseString(List<Notification> notifications)
    {
        List<string> notificationStrings = notifications.Select
            (n => $"Key: {n.Key}, reason: {n.Value}").ToList();

        string erros = string.Join("\n", notificationStrings);
        return $"Erros: \n  {erros}";
    }
}
