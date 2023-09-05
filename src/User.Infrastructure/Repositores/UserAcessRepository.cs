using Microsoft.EntityFrameworkCore;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Enums;
using User.Domain.Repositores;

namespace User.Infrastructure.Repositores;

internal class UserAcessRepository : IUserAcessRepository
{
    private readonly UserContext _userContext;
        
    public UserAcessRepository(UserContext userContext) 
        =>  _userContext = userContext;

    public IUnitOfWork UnitOfWork => _userContext;

    public void Add(UserAcess Entity) => _userContext.Add(Entity);
    
    public Task<UserAcess?> Get(Guid id, CancellationToken cancellation = default)
        => _userContext.UserAcesses.FirstOrDefaultAsync(e => e.Id == id 
        && e.Status == Status.Enable);

    public IEnumerable<UserAcess> GetAll(int itemsPerPage = 10)
        => _userContext.UserAcesses.Where(e => e.Status == Status.Enable)
        .OrderByDescending(e => e.DtCreation).Take(itemsPerPage);        

    public Task<UserAcess?> GetUserByEmail(Email email, 
        CancellationToken cancellationToken = default)
    {
        return _userContext.UserAcesses
            .FirstOrDefaultAsync(e => e.Email.Value == email.Value, cancellationToken);
    }

    public Task<UserAcess?> ValidateUser(Email email, Password password, 
        CancellationToken cancellationToken = default)
    {
        return _userContext.UserAcesses.FirstOrDefaultAsync(e => 
            ( e.Status == Status.Enable && email.Value == e.Email.Value 
               &&  e.Password.Value == password.Value) ,cancellationToken);
    }
}
