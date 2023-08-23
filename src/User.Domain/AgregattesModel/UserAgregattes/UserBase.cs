using User.Domain.Abstracts;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;

namespace User.Domain.AgregattesModel.UserAgregattes;

public abstract class UserBase : Entity
{
    public Email Email { get; private set; }
    public string Name { get; private set; }
    public DateTime DtCreation { get; }
    public Status Status { get; private set; }

    protected UserBase(
        string name,
        string email 
        )
    {
        Name = name;
        Email = Email.Create(email);
        DtCreation = DateTime.Now;
        Status = Status.Enable;

        this.Validate();
    }

    public override void Validate()
    {
        if (string.IsNullOrEmpty(Name) == true)
            AddNotification(new Notification("Name", "Nome não pode ser nulo ou vazio"));

        Email.Validate();        
        AddNotification(Email.Notifications.ToList());        

        base.Validate();
    }
}
