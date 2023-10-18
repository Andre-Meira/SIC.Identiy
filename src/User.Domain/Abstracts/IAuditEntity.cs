using User.Domain.Extensions;
using System.Diagnostics;
using User.Domain.Enums;

namespace User.Domain.Abstracts;

public interface IAuditEntity
{       
    Guid? TokenUser => Activity.Current?.GetCurrentUser();
    ActivityTraceId? TraceId => Activity.Current?.TraceId;
    DateTime? CreatedAt => DateTime.UtcNow;    
    Guid IdEntityDomain => _IdEntityDomain;
    
    protected Guid _IdEntityDomain { get; }
}


