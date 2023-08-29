using MediatR;
using User.Application.DTO;

namespace User.Application.Queries;

public record GetUserQuerie : IRequest<UserAcessDTO>
{
    public Guid Id { get;  set; }
}

public record GetAllUserQuerie : IRequest<List<UserAcessDTO>>
{
    public int Range { get; set; }
}