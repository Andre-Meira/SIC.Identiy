using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;

namespace User.Domain.AgregattesModel;

public abstract class UserBase : Entity
{
    private readonly UserAcess _userAcess = null!;
    public UserAcess UserAcess => _userAcess;
    
    public Guid IdUser { get; private set; }
    public string Name { get; private set; }
    
    public UserBase(string name) => Name = name;
}
