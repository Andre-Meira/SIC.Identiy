using Identiy.Domain.Abstracts;
using System.Net.Mail;

namespace Identiy.Domain.AgregattesModel.ValueObjects;

public record Email 
{
    public Email(string value)
    {        
        Value = value;         
    }

    public string Value { get; private set; }

    public static Email Create(string email) => new Email(email); 

    public void Validate(INotificationDomain notification)
    {
        if (string.IsNullOrWhiteSpace(Value))
            notification.AddNotification(new Notification("Email", "Email não pode ser nulo ou vazio."));

        try
        {
            new MailAddress(Value);                        
        }
        catch
        {
            notification.AddNotification(new Notification("Email", "Email está no formato invalido."));
        }
    }

}
