using System.ComponentModel.Design;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes.EventsDomain;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class UserAcess : Entity, IAggregate
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string Name { get; private set; }
    public DateTime DtCreation { get; }    
    public Status Status { get; private set; }
    public UserTypeEnum UserType { get; }

    protected UserAcess(
        string name,
        string email,
        string password,
        UserTypeEnum userType = UserTypeEnum.Client
        )
    {
        Name = name;
        Password = Password.Create(password);
        Email = Email.Create(email);
        DtCreation = DateTime.Now;
        Status = Status.Enable;        
        UserType = userType;    
    }

    public override void Validate()
    {
        if (string.IsNullOrEmpty(Name) == true)
            AddNotification(new Notification("Name", "Nome não pode ser nulo ou vazio"));

        Email.Validate();        
        AddNotification(Email.Notifications.ToList());
        Password.Validate();
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
        

        if (UserType == UserTypeEnum.Client)
        {
            
            RaiseDomainEvent(new UserClientCreatedDomainEvent(Id));
        }
        else 
        {
        
        }        
    }

}
