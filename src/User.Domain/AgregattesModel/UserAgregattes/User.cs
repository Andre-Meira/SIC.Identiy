using User.Domain.Abstracts;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class User : Entity
{
    private readonly List<Incident> _incidents;

    private Password _password;

    public User(
        string name, 
        string email, 
        string password)
    {
        Name = name;
        Email = Email.Create(email);
        _password = Password.Create(password);

        DtCreation = DateTime.Now;
        Status = Status.Enable;

        _incidents = new List<Incident>();

        Validate();
    }

    public string Name { get; private set; }
    public Email Email { get; private set;  }
    private Password Password => _password;
    public DateTime DtCreation { get; }
    public Status Status { get; private set; }
     
    //public IReadOnlyCollection<Incident>? Incidentes => _incidents;

    //public void RaiseIncident(Incident incidente)
        //=> _incidents.Add(incidente);

    public void ChangePassword(string password)
    {        
        Password newPassword = Password.Create(password);

        if (Password.Equals(newPassword) == true)
            throw new DomainExceptions("A senha não pode ser igual a anterior");

        _password = newPassword;       
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
}


public record Incident(Guid IdIncidente, StutusIncident StutusIncident);