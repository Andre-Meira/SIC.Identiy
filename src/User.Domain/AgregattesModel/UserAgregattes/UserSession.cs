using User.Domain.Abstracts;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class UserSession : Entity
{
    public UserSession(
        Guid idUser, 
        string email,         
        string token)
    {
        IdUser = idUser;
        Email = Email.Create(email);
        DtCreate = DateTime.Now;
        Token = token;
    }

    public Guid IdUser { get; private set; }
    public Email Email { get; private set; }
    public DateTime DtCreate { get;}
    public string Token { get; private set; }
}
