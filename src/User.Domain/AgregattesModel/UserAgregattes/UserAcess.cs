using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes.EventsDomain;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class UserAcess : Entity, IAggregate
{    
    public virtual Email Email { get; private set; }
        
    public virtual Password Password { get; private set; }
    
    public string Name { get; private set; }
    public DateTime DtCreation { get; }
    public Status Status { get; private set; }
    public UserTypeEnum UserType { get; }

    protected UserAcess() { }

    public UserAcess(
        string name,
        string email,
        string password,
        UserTypeEnum userType = UserTypeEnum.Client
        )
    {
        Name = name;
        Password = Password.Create(password);
        Email = Email.Create(email);
        DtCreation = DateTime.Now.ToUniversalTime();
        Status = Status.Enable;
        UserType = userType;

        Validate();
    }

    public override void Validate()
    {
        if (string.IsNullOrEmpty(Name) == true)
            AddNotification(new Notification("Name", "Nome não pode ser nulo ou vazio"));

        Email.Validate();
        Password.Validate();

        AddNotification(Email.Notifications.ToList());
        AddNotification(Password.Notifications.ToList());

        base.Validate();
    }

    public void ChangePassword(string password)
    {
        Password newPassword = Password.Create(password);

        if (Password.Equals(newPassword) == true)
            throw new DomainExceptions("A senha não pode ser igual a anterior");

        Password = newPassword;
    }

    public override void Create()
    {
        Validate();
        base.Create();

        if (UserType == UserTypeEnum.Client)
        {
            var createdEvent = new UserClientCreatedDomainEvent(Id, Name, Email.Value);
            RaiseDomainEvent(createdEvent);
        }
        else
        {
            var createdEvent = new UserAttendantCreatedDomainEvent(Id, Name, Email.Value);
            RaiseDomainEvent(createdEvent);
        }
    }

}
