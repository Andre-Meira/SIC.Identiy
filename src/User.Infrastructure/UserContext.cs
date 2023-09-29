using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Domain.Abstracts;
using User.Domain.AgregattesModel.AttendantAgregattes;
using User.Domain.AgregattesModel.ClientAgregattes;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Infrastructure;

internal class UserContext : DbContext, IUnitOfWork
{
    
    public DbSet<UserAcess> UserAcesses { get; private set; } = null!;

    public DbSet<Attendant> Attendant { get; private set; } = null!;

    public DbSet<Client> Client { get; private set; } = null!;
    
    private readonly IMediator _mediator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<UserContext> _logger;  

    public UserContext(
        DbContextOptions<UserContext> options,
        ILoggerFactory loggerFactory,
        IMediator mediator,
        ILogger<UserContext> logger
        ) : base(options)
    {
        _mediator = mediator;
        _loggerFactory = loggerFactory;        
        _logger = logger;   
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(_loggerFactory)
            .EnableSensitiveDataLogging(true)
            .EnableDetailedErrors(true)            
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);            

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Owned<Email>();
        modelBuilder.Owned<Password>();

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entity.ClrType)
                .Ignore(nameof(Entity.IsDeleted));                
        }       

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }


    public async Task SaveChangesEntity(CancellationToken cancellationToken = default)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception err)
        {
            _loggerFactory.CreateLogger<UserContext>()
                .LogError($"Falha ao savar os registros {err.Message}");

            throw;
        }
    }   

    private async Task DispatchDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.GetEvents()?.Count > 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.GetEvents())
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            try
            {
                await _mediator.Publish(domainEvent);
            }
            catch (Exception err)
            {
                _loggerFactory.CreateLogger<UserContext>().LogError(err.Message);
            }
        }        
    }


    private void SetEntityState()
    {
        foreach (var entry in ChangeTracker.Entries().ToArray())
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified) continue;
            
            if (!(entry.Entity is Entity entity)) continue;

            if (entry.State == EntityState.Added) entity.Create();
            else if (entity.IsDeleted) entry.State = EntityState.Deleted;
        }
    }

}
 