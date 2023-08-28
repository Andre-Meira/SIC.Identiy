using User.API.Filter;
using User.Application;
using User.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogginSerilog();

builder.Services.AddControllers(e =>
{
    e.Filters.Add(typeof(ExceptionCustomFIlter));
});

builder.Services.ConfigureLogging();
builder.Services.AddTracing();

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