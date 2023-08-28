using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using User.Domain.Abstracts;

namespace User.Application;

internal class LoggingBehaviors<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest   
{
    private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, 
        TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            
            _logger.LogInformation($"Send Request {typeof(TRequest).Name}, {DateTime.Now}");
            var result = await next();            
            return result;
        }
        catch (Exception err) when (err is not DomainExceptions) 
        {
            _logger.LogError($"Request Error {typeof(TRequest).Name}, {DateTime.Now}, " +
                $"n\' reson: {err.Message}");

            throw;
        }
        finally 
        {
            stopwatch.Stop();
            _logger.LogInformation($"Complet Request {typeof(TRequest).Name}, {DateTime.Now}, duration: {stopwatch.Elapsed}");
        }        
    }
}
