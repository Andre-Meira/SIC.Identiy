using Microsoft.AspNetCore.Mvc;
using User.API.Exceptions;
using User.API.Models;
using User.Domain.Abstracts;


namespace User.API.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception err)
        {
            await HandlerExectionFilterAsync(context, err);
        }
    }

    public async Task HandlerExectionFilterAsync(HttpContext context, Exception exception)
    {
        if (exception is DomainExceptions)
        {
            DomainExceptions domainExceptions = (DomainExceptions)exception;
            object data = domainExceptions.Message;

            if (domainExceptions.Messages?.Count > 0)
                data = domainExceptions.Messages;

            ResultController resultController = new ResultController("Error", 400, data);
            await context.Response.WriteAsJsonAsync(resultController);
        }

        if (exception is ExceptionRequest)
        {
            ExceptionRequest domainExceptions = (ExceptionRequest)exception;
            object data = domainExceptions.Data;
            int code = (int)domainExceptions.StatusCode;

            ResultController resultController = new ResultController(domainExceptions.Message, code, data);
            await context.Response.WriteAsJsonAsync(resultController);
        }

        if (exception is not DomainExceptions && exception is not ExceptionRequest)
        {
            _logger.LogError(exception, $"Request error: {exception.Message}");

            ResultController resultController = new ResultController("Error", 500, exception.Message);
            ObjectResult badRequest = new ObjectResult(resultController) { StatusCode = 500 };
            await context.Response.WriteAsJsonAsync(resultController);
        }
    }
}
