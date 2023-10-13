using Microsoft.EntityFrameworkCore;
using User.Domain.Enums;

namespace User.Infrastructure.Helpers;

public static class StateHelper
{
    public static AuditType ConvertStateEntity(this EntityState entity)
    {
        switch (entity)
        {            
            case EntityState.Deleted:
                return AuditType.Delete;
            case EntityState.Modified:
                return AuditType.Update;
            case EntityState.Added:
                return AuditType.Create;
            default:
                throw new ArgumentException(nameof(entity));
        }
    }
}
