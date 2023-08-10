namespace Identiy.Domain.Abstracts;

public class DomainExceptions : Exception
{
    public List<string>? Messages { get; }

    public DomainExceptions() { }    

    public DomainExceptions(string message) : base(message) { }    

    public DomainExceptions(string message, Exception innerException) 
        : base(message, innerException) { }
    
    public DomainExceptions(List<string> messages) 
        : base(messages.ToString()) => Messages = messages;

}
