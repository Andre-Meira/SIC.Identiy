using MongoDB.Bson;
using System.Diagnostics;
using User.Domain.Abstracts;
using User.Domain.Enums;

namespace User.Infrastructure.Models;

internal class AuditStoreDatabase
{
    public AuditStoreDatabase(Guid? tokenUser, 
        ActivityTraceId? traceId, 
        DateTime? createdAt, 
        AuditType type, 
        Guid idEntityDomain,
        string data)
    {
        TokenUser = tokenUser;
        TraceId = traceId;
        CreatedAt = createdAt;
        Type = type;
        IdEntityDomain = idEntityDomain;
        Data = data;
    }

    public AuditStoreDatabase() { }

    public Guid? TokenUser { get; private set; }
    public ActivityTraceId? TraceId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public AuditType Type { get; private set; }
    public Guid IdEntityDomain { get ; private set; }

    public string? Data { get; private set; }

    public static AuditStoreDatabase Create(IAuditEntity audit, string data, AuditType auditType)
    {
        return new AuditStoreDatabase
        {
            CreatedAt = audit.CreatedAt,
            Data = data,
            IdEntityDomain = audit.IdEntityDomain,
            Type = auditType,
            TraceId = audit.TraceId,
            TokenUser = audit.TokenUser
        };
    }
}
