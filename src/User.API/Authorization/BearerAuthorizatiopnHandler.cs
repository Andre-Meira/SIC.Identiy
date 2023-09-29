using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using User.API.Exceptions;
using User.API.Models;
using User.Application.Services;
using User.Domain.Abstracts;
using User.Domain.Extensions;

namespace User.API.Authorization;

public class BearerAuthorizationHandler : AuthorizationHandler<BearerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        BearerRequirement requirement)
    {
        JwtProvider token =  JwtProvider.GetToken(context.User);

        if (token.IsAuthenticate == false)
        {
            // TODO Melhorar estrutura de erro.
            throw new ExceptionRequest("Sem acesso",
                "Você não tem permissão para acessar este recurso.", 
                HttpStatusCode.Unauthorized);            
        }

        Activity.Current?.SetUser(token.Id);
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

