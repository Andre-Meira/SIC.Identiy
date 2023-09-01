using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using User.Domain.Extensions;

namespace User.Infrastructure.Interceptors;

//TODO MELHOR AUDITORIA
internal sealed class AuditEntityInterceptors : SaveChangesInterceptor
{
    private readonly ILogger<AuditEntityInterceptors> _logger;

    public AuditEntityInterceptors(ILogger<AuditEntityInterceptors> logger)
    {
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var trackers = eventData.Context?.ChangeTracker;
        _logger.LogInformation($"O usuario {Activity.Current?.GetUser()} solicitou a seguintes alteração:" +
            $" { trackers?.DebugView.LongView}");

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, 
        CancellationToken cancellationToken = default)
    {        
        _logger.LogInformation($"Saving changes failed.");
        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}