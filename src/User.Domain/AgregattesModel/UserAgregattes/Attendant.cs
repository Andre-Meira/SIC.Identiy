
using User.Domain.Abstracts;

namespace User.Domain.AgregattesModel.UserAgregattes;

public class Attendant : Entity
{
    public UserAcess UserAcess { get; private set; }


    public Attendant()
    {
        
    }
}
