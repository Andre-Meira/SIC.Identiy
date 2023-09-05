using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using User.Domain.Abstracts;

namespace User.Application;

internal class LoggingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, 
        TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {

            _logger.LogInformation("Send Request {NameCommand}, {Date}", 
                typeof(TRequest).Name, DateTime.Now);

            var result = await next();
            return result;
        }
        catch (Exception err) when (err is not DomainExceptions)
        {
            _logger.LogError("Request {NameCommand} {Date}, erro {Error}",
                    typeof(TRequest).Name, DateTime.Now, err.Message);

            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Complet Request {NameCommand}, {Date}, duration: {DateFinish}",
                typeof(TRequest).Name, DateTime.Now, stopwatch.Elapsed);
        }
    }
}
