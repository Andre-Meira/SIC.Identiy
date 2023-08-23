namespace User.Domain.AgregattesModel.UserAgregattes;

public sealed class Client : UserBase
{
    public bool AcceptedNotification { get; private set; }     

    public Client(string nome, string email) 
        : base(nome, email) { }
    
}
