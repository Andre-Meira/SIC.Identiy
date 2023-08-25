using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using User.Domain.Abstracts;

namespace User.Domain.AgregattesModel.ValueObjects;

[NotMapped]
public record Email(string Value) : ValueObject
{           
    public static Email Create(string email) => new Email(email); 

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            AddNotification(new Notification("Email", "Email não pode ser nulo ou vazio."));
            return;
        }

        try
        {
            new MailAddress(Value);
        }
        catch
        {
            AddNotification(new Notification("Email", "Email está no formato invalido."));
        }
    }
}
