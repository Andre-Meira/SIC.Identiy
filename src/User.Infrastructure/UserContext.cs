using Microsoft.EntityFrameworkCore;
using User.Domain.Abstracts;

namespace User.Infrastructure;

internal class UserContext : DbContext, IUnitOfWork
{
    public Task SaveChangesEntity()
    {
        return Task.CompletedTask;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entity.ClrType).Ignore(nameof(Entity.IsDeleted));

        }

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
