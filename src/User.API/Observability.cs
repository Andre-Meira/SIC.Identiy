using Microsoft.AspNetCore.Mvc.ApplicationParts;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace User.API;

public static class Observability
{
    public static ConfigureHostBuilder AddLogginSerilog(this ConfigureHostBuilder host, ConfigurationObservability config)
    {        
        host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console(outputTemplate: @"[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {Properties:j} Mensagem:{Message:lj}{NewLine}{Exception}");            
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
            configuration.Enrich.FromLogContext();

            if (config.EndpointLoggin != null)
                configuration.WriteTo.Seq(config.EndpointLoggin.ToString());

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
    
    public static OpenTelemetryBuilder AddTracing(this IServiceCollection services, 
        ConfigurationObservability configure)
    {
         return services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault()
                   .AddService("User.API");

                tracing.AddAspNetCoreInstrumentation(asp =>
                {
                    asp.Filter = FilterReques;
                    asp.EnrichWithHttpRequest = (Activity activity, HttpRequest request) =>
                    {
                        activity.AddTag("TraceId", activity.TraceId);
                    };
                });
                tracing.AddOtlpExporter(e =>
                {
                    e.Endpoint = configure.EndpointOtlp;
                    e.Protocol = OtlpExportProtocol.Grpc;
                });
                tracing.SetResourceBuilder(resourceBuilder);
            });
    }

    private static bool FilterReques(HttpContext http)
    {
        return !http.Request.Path.Value!.Contains("/swagger")
            & !http.Request.Path.Value!.Contains("_framework")
            & !http.Request.Path.Value!.Contains("_vs");
    }
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

public class ConfigurationObservability
{
    public Uri? EndpointOtlp { get; set; }

    public Uri? EndpointLoggin { get; set; }

    public string ApplicationName { get; set; }

    public ConfigurationObservability(string applicationName)
    {
        ApplicationName = applicationName;  
    }
}