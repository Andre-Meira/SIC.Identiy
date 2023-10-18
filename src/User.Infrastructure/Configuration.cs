using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;
using User.Domain.AgregattesModel.ValueObjects;
using User.Domain.Repositores;
using User.Infrastructure.Abstract;
using User.Infrastructure.Interceptors;
using User.Infrastructure.Models;
using User.Infrastructure.Repositores;

namespace User.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructureContext(this IServiceCollection service)
    {
        string? connectionString = Environment.GetEnvironmentVariable("connectionString");
        if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));


        service.Configure<AuditStoreDatabaseSettings>(e =>
        {
            e.DatabaseName = "User";
            e.ConnectionString = "mongodb://localhost:27017";
            e.BooksCollectionName = "Audit";
        });

        service.AddSingleton<AuditEntityInterceptors>();
        service.AddSingleton<EventsDomainEntityInceptors>();
        service.AddSingleton<IAuditDomainService, AuditDomainService>();

        service.AddDbContext<UserContext>((sp,options)=> 
        {            
            options.AddInterceptors(sp.GetService<AuditEntityInterceptors>()!);
            options.AddInterceptors(sp.GetService<EventsDomainEntityInceptors>()!);

            options.UseNpgsql(connectionString);
        },ServiceLifetime.Transient);

        service.AddRepositores();

        return service;
    }

    private static IServiceCollection AddRepositores(this IServiceCollection services)
    {
        services.AddScoped<IUserAcessRepository, UserAcessRepository>();

        return services;
    }
}
