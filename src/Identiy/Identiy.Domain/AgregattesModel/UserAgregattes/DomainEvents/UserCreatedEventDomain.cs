using Identiy.Domain.Abstracts;

namespace Identiy.Domain.AgregattesModel.UsuarioAgregattes.Events;

public sealed record UserCreatedEventDomain(Guid id, string name, string email) : IDomainEvent;
