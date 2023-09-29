using Newtonsoft.Json;
using System.Diagnostics;
using User.Domain.Extensions;

namespace User.Domain.Abstracts;

public interface IAuditEntity
{       
    Guid? TokenUser => Activity.Current?.GetCurrentUser();
    ActivityTraceId? TraceId => Activity.Current?.TraceId;
}


