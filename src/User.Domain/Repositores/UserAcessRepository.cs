using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Domain.Repositores;

public interface IUserAcessRepository : IRepository<UserAcess>
{
    Task<UserAcess?> GetUserByEmail(Email email, CancellationToken cancellationToken = default);
    Task<UserAcess?> ValidateUser(Email email, Password password, CancellationToken cancellationToken = default);
}
