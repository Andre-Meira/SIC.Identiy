using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.Infrastructure;

namespace User.Application;

public static class Implementation 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviors<,>));
        services.AddMediatR(e => e.RegisterServicesFromAssembly(assembly));        

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services.AddInfrastructureContext();
}

