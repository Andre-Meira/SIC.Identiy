using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using User.Domain.Abstracts;
using User.Infrastructure.Models;
using User.Infrastructure.Abstract;

namespace User.Infrastructure.Interceptors;

//TODO MELHOR AUDITORIA
internal sealed class AuditEntityInterceptors : SaveChangesInterceptor
{
    private readonly ILogger<AuditEntityInterceptors> _logger;

    private readonly IAuditDomainService _auditDomainService;

    public AuditEntityInterceptors(
        ILogger<AuditEntityInterceptors> logger,
        IAuditDomainService auditDomainService
    )
    {
        _logger = logger;
        _auditDomainService = auditDomainService;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {           
        ChangeTracker? trackers = eventData.Context?.ChangeTracker;

        InterceptionResult<int> interceptionResult = await base.SavingChangesAsync(
            eventData,result, cancellationToken);

        if (trackers is null) return interceptionResult;        

        IEnumerable<EntityEntry> entitesTracker = trackers.Entries<IAuditEntity>()
            .Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached);

        await AuditEntityDomain(entitesTracker, cancellationToken);
        return interceptionResult;
    }

    private async Task AuditEntityDomain(IEnumerable<EntityEntry> entities,
        CancellationToken cancellation = default)
    {                  
        foreach (EntityEntry entity in entities)
        {                       
            AuditStoreDatabase auditStore = AuditStoreDatabase.Create(entity);
            await _auditDomainService.CreateAsync(auditStore, cancellation).ConfigureAwait(false);
        }        
    }
}