using User.API.Filter;
using User.Application;
using User.API;
using User.API.Authorization;

string endpointSeq = Environment.GetEnvironmentVariable("SeqLogging") 
    ?? throw new ArgumentNullException(nameof(endpointSeq));

string endpoinOtlp = Environment.GetEnvironmentVariable("OtlpEndPoint") 
    ?? throw new ArgumentNullException(nameof(endpoinOtlp));

ConfigurationObservability configuration = new ConfigurationObservability("User.API");
configuration.EndpointLoggin = new Uri(endpointSeq);
configuration.EndpointOtlp = new Uri(endpoinOtlp);

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogginSerilog(configuration);


builder.Services.AddControllers(e =>
{
    e.Filters.Add(typeof(ExceptionCustomFIlter));
});

builder.Services.AddUserAuthentication();
builder.Services.ConfigureLogging();
builder.Services.AddTracing(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();