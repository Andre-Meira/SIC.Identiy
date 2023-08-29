using MediatR;
using User.Application.Commands;
using User.Application.Services;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Repositores;

namespace User.Application.UseCases;

public class TokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
{
    public readonly IUserAcessRepository _repository;

    public TokenCommandHandler(IUserAcessRepository repository)
         => _repository = repository;

    public async Task<string> Handle(CreateTokenCommand request, 
        CancellationToken cancellationToken)
    {
        Email emailUser = Email.Create(request.UserName);
        Password passwordUser = Password.Create(request.Password);

        UserAcess? acess = await _repository.ValidateUser(emailUser, passwordUser, cancellationToken);

        if (acess == null) throw new DomainExceptions("Usuario não encontrado.");
        
        return new JwtProvider(acess.Id, acess.Name, acess.Email.Value).GenerateToken();
    }
}
