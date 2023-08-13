using Identiy.Domain.Abstracts;
namespace Identiy.Domain.AgregattesModel.CompanyAgregattes.Events;

public sealed record CompanyDisabledEventDomain(Guid id, string name, string cnpj) : IDomainEvent;
