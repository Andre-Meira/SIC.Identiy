using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices.ObjectiveC;
using User.API.Models;
using User.Domain.Abstracts;

namespace User.API.Filter;

public class ExceptionCustomFIlter : IExceptionFilter
{
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
    }
}
