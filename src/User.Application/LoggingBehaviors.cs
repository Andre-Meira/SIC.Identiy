using MediatR;
using Microsoft.Extensions.Logging;

namespace User.Application;

internal class LoggingBehaviors<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>   
{
    private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, 
        TResponse>> logger)
    {
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
