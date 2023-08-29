using MediatR;

namespace User.Application.Commands;

public record CreateTokenCommand(string UserName, string Password) : IRequest<string>;

