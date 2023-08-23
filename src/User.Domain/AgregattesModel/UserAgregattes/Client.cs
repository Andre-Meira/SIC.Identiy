namespace User.Domain.AgregattesModel.UserAgregattes;

public sealed class Client : User
{
    public bool AcceptedNotification { get; private set; }

    public Client(string nome, string email, string password) 
        : base(nome, nome, password) { }
    
}
