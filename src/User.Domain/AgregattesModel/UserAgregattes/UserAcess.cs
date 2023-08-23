using User.Domain.Abstracts;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class UserAcess : UserBase
{
    public UserAcess(
        string name, 
        string email, 
        string password) 
        : base(name, email)
    {
        Password = Password.Create(password);
        Validate();
    }

    public Password Password { get; private set; }

    public void ChangePassword(string password)
    {        
        Password newPassword = Password.Create(password);

        if (Password.Equals(newPassword) == true)
            throw new DomainExceptions("A senha não pode ser igual a anterior");

        Password = newPassword;       
    }


    public override void Validate()
    {       
        Password.Validate();       
        AddNotification(Password.Notifications.ToList());
        base.Validate();
    }
}
