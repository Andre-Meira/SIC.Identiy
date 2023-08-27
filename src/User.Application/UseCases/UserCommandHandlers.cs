using MediatR;
using User.Application.Commands;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Repositores;

namespace User.Application.UseCases;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserAcessRepository _userAcessRepository;

    public CreateUserCommandHandler(IUserAcessRepository userAcessRepository)
        => _userAcessRepository = userAcessRepository;

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Email email = Email.Create(request.Email);
        UserAcess? userAcess = await _userAcessRepository.GetUserByEmail(email, cancellationToken);

        if (userAcess != null) throw new DomainExceptions("Já existe um usuario com esse Email");

        UserAcess user = new UserAcess(request.Name, request.Email, request.Password, request.UserType);
        user.Validate();

        _userAcessRepository.Add(user);
        await _userAcessRepository.UnitOfWork.SaveChangesEntity(cancellationToken);
    }
}
