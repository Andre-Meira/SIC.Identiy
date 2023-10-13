using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using User.Domain.Abstracts;
using User.Infrastructure.Helpers;
using User.Domain.Enums;
using User.Infrastructure.Models;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace User.Infrastructure.Interceptors;

//TODO MELHOR AUDITORIA
internal sealed class AuditEntityInterceptors : SaveChangesInterceptor
{
    private readonly ILogger<AuditEntityInterceptors> _logger;

    private readonly AuditDomainService _auditDomainService;

    public AuditEntityInterceptors(
        ILogger<AuditEntityInterceptors> logger, 
        AuditDomainService auditDomainService)
    {
        _logger = logger;
        _auditDomainService = auditDomainService;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {        
        ChangeTracker? trackers = eventData.Context?.ChangeTracker;

        if (trackers is null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        InterceptionResult<int> interceptionResult = await base.SavingChangesAsync(eventData, 
            result, cancellationToken);

        IEnumerable<EntityEntry> entitesTracker = trackers.Entries<IAuditEntity>()
            .Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached);

        await AuditEntityDomain(entitesTracker);

        return interceptionResult;
    }

    private async Task AuditEntityDomain(IEnumerable<EntityEntry>  entities)
    {                  
        foreach (EntityEntry entity in entities)
        {
            IAuditEntity auditEntity = (IAuditEntity)entity.Entity;
            AuditType auditType = entity.State.ConvertStateEntity();

            if (auditEntity.AuditTypes.Contains(auditType) == false) continue;


            string jsonData = JsonConvert.SerializeObject(entity.Entity); 

            AuditStoreDatabase auditStore = AuditStoreDatabase.Create(auditEntity, jsonData, auditType);

            await _auditDomainService.CreateAsync(auditStore);
        }        
    }
}