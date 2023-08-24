
using User.Domain.Abstracts;

namespace User.Domain.AgregattesModel.UserAgregattes.EventsDomain;

/// <summary>
///     Send event when user of type client is created
/// </summary>
/// <param name="Id">Id User created</param>
/// <param name="Name">user name</param>
/// <param name="Email"></param>
public record UserClientCreatedDomainEvent
    (Guid Id, string Name, string Email) : IDomainEvent;

/// <summary>
///  Send event when user of type client is Attendant
/// </summary>
/// <param name="Id">Id User created</param>
/// <param name="Name"></param>
/// <param name="Email"></param>
public record UserAttendantCreatedDomainEvent
    (Guid Id, string Name, string Email) : IDomainEvent;


/// <summary>
/// Send event when user is disabled
/// </summary>
/// <param name="Id">ID User disable</param>
/// <param name="reason">reason of user disable</param>
/// <param name="Email">user mail</param>
public record UserDisabledDomainEvent
    (Guid Id, string reason, string Email) : IDomainEvent;
