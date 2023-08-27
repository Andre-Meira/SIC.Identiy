using OpenTelemetry;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using User.API.Filter;
using User.Application;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
;

var builder = WebApplication.CreateBuilder(args);



builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}");
    configuration.WriteTo.OpenTelemetry();
    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    configuration.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
    configuration.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Information);
    configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information);    
    configuration.Enrich.FromLogContext();            

});

builder.Services.AddLogging(e =>
{        
    e.AddSerilog(logger: Log.Logger, dispose: true);    
    e.AddEventSourceLogger();
});

builder.Services.AddControllers(e =>
{
    e.Filters.Add(typeof(ExceptionCustomFIlter));
});

builder.Services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault()
                   .AddService("User.API");
                

                tracing.AddAspNetCoreInstrumentation(e =>
                {
                    e.EnrichWithHttpRequest = 
                });
                tracing.SetResourceBuilder(resourceBuilder);
                tracing.AddZipkinExporter("User.API",config =>
                {
                    
                });
            });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();