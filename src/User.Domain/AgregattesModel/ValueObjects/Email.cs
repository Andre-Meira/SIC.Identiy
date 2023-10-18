using System.Net.Mail;
using User.Domain.Abstracts;

namespace User.Domain.AgregattesModel.ValueObjects;

public record Email(string Value) : ValueObject
{           
    public static Email Create(string email) => new Email(email); 

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value)) throw new DomainExceptions("Email não pode ser nulo ou vazio.");

        try
        {
            new MailAddress(Value);
        }
        catch
        {
            throw new DomainExceptions("Email está no formato invalido.");
        }
    }
}
