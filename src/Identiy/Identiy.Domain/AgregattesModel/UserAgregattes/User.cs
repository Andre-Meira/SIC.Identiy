using Identiy.Domain.Abstracts;
using Identiy.Domain.AgregattesModel.CompanyAgregattes;
using Identiy.Domain.AgregattesModel.UserAgregattes.DomainEvents;
using Identiy.Domain.AgregattesModel.ValueObjects;
using Identiy.Domain.Enums;

namespace Identiy.Domain.AgregattesModel.UserAgregattes;

public class User : Entity, IAggregate
{
    public User(string name, 
        string email, 
        string login, 
        string password)
    {
        Name = name;        
        Email = Email.Create(email);
        Login = login;

        Password = Password.Create(password);
        DtCreated = DateTime.Now;
        Status = Status.Enable;          
    }

    public string Name { get; private set; }

    public Email Email { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public Status Status { get; private set; }

    public Company? Company { get; private set; }

    public DateTime DtCreated { get; }


    public override void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            AddNotification(new Notification("Name", "Nome não pode ser nulo ou vazio"));

        if (string.IsNullOrEmpty(Login))
            AddNotification(new Notification("Login", "Login não pode ser nulo ou vazio"));

        Email.Validate(this);
        base.Validate();
    }

    public void Disable()
    {
        Status = Status.Disable;
        RaiseDomainEvent(new UserDisabledEventDomain(Id, Name, Email.Value));
    }


    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return (obj as User)!.Login == Login &&
          (obj as User)!.Password == Password;
    }

    public override int GetHashCode() =>  Id.GetHashCode();
}
