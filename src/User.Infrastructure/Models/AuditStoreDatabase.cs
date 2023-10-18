using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using User.Domain.Abstracts;
using User.Domain.Enums;
using User.Infrastructure.Helpers;

namespace User.Infrastructure.Models;

public sealed class AuditStoreDatabase
{
    public string? TokenUser { get; private set; }
    public string? TraceId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public string? Type { get; private set; }
    public string? IdEntityDomain { get ; private set; }
    public string? TableName { get; private set; }   
    public EntitiyAudit? Entitiy { get; private set; }

    public static AuditStoreDatabase Create(EntityEntry entityEntry)
    {
        IAuditEntity auditEntity = (IAuditEntity)entityEntry.Entity;
        AuditType auditType = entityEntry.State.ConvertStateEntity();
        
        IDictionary<string, object?> originalEntity = new Dictionary<string, object?>();
        IDictionary<string, object?> currentEntity = new Dictionary<string, object?>();

        foreach (PropertyEntry properties in entityEntry.Properties.Where(e => 
                e.CurrentValue is not null 
                && e.OriginalValue is not null))
        {
            originalEntity.Add(properties.Metadata.Name, properties.OriginalValue);
            currentEntity.Add(properties.Metadata.Name, properties.CurrentValue);
        }

        foreach (ReferenceEntry reference in entityEntry.References.Where(e =>
                e.CurrentValue is not null))
        {
            originalEntity.Add(reference.Metadata.Name, reference.TargetEntry?.OriginalValues.ToObject());
            currentEntity.Add(reference.Metadata.Name, reference.CurrentValue);
        }

        string jsonOriginal = JsonConvert.SerializeObject(originalEntity, Formatting.Indented);
        string jsonCurrent = JsonConvert.SerializeObject(currentEntity, Formatting.Indented);

        EntitiyAudit entityAudit = new EntitiyAudit(jsonOriginal, jsonCurrent);

        return new AuditStoreDatabase
        {
            CreatedAt = auditEntity.CreatedAt,
            Entitiy = entityAudit,            
            IdEntityDomain = auditEntity.IdEntityDomain.ToString(),
            Type = auditType.ToString(),
            TraceId = auditEntity.TraceId.ToString(),
            TokenUser = auditEntity.TokenUser.ToString(),
            TableName = entityEntry.Metadata.GetTableName()
        };
    }
}

public sealed record EntitiyAudit(string OrignalValue, string CurrentValue);


