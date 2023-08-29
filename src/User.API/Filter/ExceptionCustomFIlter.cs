using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using User.API.Exceptions;
using User.API.Models;
using User.Domain.Abstracts;


namespace User.API.Filter;

public class ExceptionCustomFIlter : IExceptionFilter
{
    private readonly ILogger<ExceptionCustomFIlter> _logger; 

    public ExceptionCustomFIlter(
        ILogger<ExceptionCustomFIlter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {       
        if (context.Exception is DomainExceptions)
        {
            DomainExceptions domainExceptions = (DomainExceptions)context.Exception;
            object data = domainExceptions.Message;

            if (domainExceptions.Messages?.Count > 0)
                data = domainExceptions.Messages;

            ResultController resultController = new ResultController("Error", 400, data);
            ObjectResult badRequest = new ObjectResult(resultController) { StatusCode = 400};            
            context.Result = badRequest;            
        }

        if (context.Exception is ExceptionRequest) 
        {
            ExceptionRequest domainExceptions = (ExceptionRequest)context.Exception;
            object data = domainExceptions.Message;
            int code = (int)domainExceptions.StatusCode;

            ResultController resultController = new ResultController("Error", code, data);
            ObjectResult badRequest = new ObjectResult(resultController) { StatusCode = code};
            context.Result = badRequest;
        }

        if(context.Exception is not DomainExceptions && context.Exception is not ExceptionRequest)
        {
            ResultController resultController = new ResultController("Error", 500, context.Exception.Message);
            ObjectResult badRequest = new ObjectResult(resultController) { StatusCode = 500};
            context.Result = badRequest;

            _logger.LogError(context.Exception, $"Request error: {context.Exception.Message}");
        }
    }
}
