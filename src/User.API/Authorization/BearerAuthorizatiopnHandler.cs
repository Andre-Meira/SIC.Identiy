using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Net;
using User.API.Exceptions;
using User.Application.Services;
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
            context.Fail(); 
            return Task.CompletedTask;
        }

        Activity.Current?.SetUser(token.Id);
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
