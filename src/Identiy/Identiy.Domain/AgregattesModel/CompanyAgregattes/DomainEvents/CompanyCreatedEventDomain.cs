using Identiy.Domain.Abstracts;

namespace Identiy.Domain.AgregattesModel.CompanyAgregattes.Events;


/// <summary>
/// Evento sera lançado quando uma nova empresa for cadastrada
/// </summary>
public sealed record CompanyCreatedEventDomain(Guid id, string companyName, string cnpj) : IDomainEvent;