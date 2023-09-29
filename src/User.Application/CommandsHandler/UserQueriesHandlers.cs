using MediatR;
using User.Application.DTO;
using User.Application.Queries;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.Repositores;

namespace User.Application.UseCases;

internal class UserQueriesHandlers : IRequestHandler<GetUserQuerie, UserAcessDTO>,
    IRequestHandler<GetAllUserQuerie, List<UserAcessDTO>>
{
    private readonly IUserAcessRepository _userAcessRepository;

    public UserQueriesHandlers(IUserAcessRepository userAcessRepository)
    {
        _userAcessRepository = userAcessRepository;
    }        

    public async Task<UserAcessDTO> Handle(GetUserQuerie request, 
        CancellationToken cancellationToken)
    {
        UserAcess? userAcess = await _userAcessRepository.Get(request.Id, cancellationToken);
        if (userAcess == null) throw new DomainExceptions("Usuario não encontrado.");

        return UserAcessDTO.ToDto(userAcess);
    }

    public Task<List<UserAcessDTO>> Handle(GetAllUserQuerie request, CancellationToken cancellationToken)
    {
        List<UserAcessDTO> usersDtos = new List<UserAcessDTO>();
        List<UserAcess> userAcesses = _userAcessRepository.GetAll(request.Range).ToList();

        userAcesses.ForEach(e => usersDtos.Add(UserAcessDTO.ToDto(e)));

        return Task.FromResult(usersDtos);
    }
}
