using User.Infrastructure.Models;

namespace User.Infrastructure.Abstract;

public interface IAuditDomainService 
{
    Task CreateAsync(AuditStoreDatabase auditStore, CancellationToken cancellation = default);
}
