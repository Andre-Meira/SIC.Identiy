using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace User.API;

public static class Observability
{
    public static ConfigureHostBuilder AddLogginSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console(outputTemplate: @"[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {Properties:j} Mensagem:{Message:lj}{NewLine}{Exception}");
            
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
            configuration.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
            configuration.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Information);
            configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information);
            configuration.Enrich.FromLogContext();
            configuration.WriteTo.Seq("http://localhost:5341");
            configuration.Enrich.With<TraceIdEnricher>();            
        });        

        return host;
    }

    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging(e =>
        {
            e.AddSerilog(logger: Log.Logger, dispose: true);
            e.AddEventSourceLogger();
        });

        return services;
    }
    
    public static OpenTelemetryBuilder AddTracing(this IServiceCollection services)
        => services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault()
                   .AddService("User.API");

                tracing.AddAspNetCoreInstrumentation();
                tracing.AddOtlpExporter();
                tracing.SetResourceBuilder(resourceBuilder);                
            });    
    
}

partial class TraceIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var currentActivity = Activity.Current;
        if (currentActivity != null)
        {
            var traceIdProperty = propertyFactory.CreateProperty("TraceId", currentActivity.TraceId);
            logEvent.AddPropertyIfAbsent(traceIdProperty);
        }
    }
}