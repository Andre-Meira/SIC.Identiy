using Identiy.Domain.Abstracts;

namespace Identiy.Domain.AgregattesModel.UserAgregattes.DomainEvents;

public sealed record UserDisabledEventDomain(Guid id, string name, string email) : IDomainEvent { }
