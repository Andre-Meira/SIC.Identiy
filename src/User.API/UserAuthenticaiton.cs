using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.API.Authorization;

namespace User.API;

public static class UserAuthenticaiton
{
    public static IServiceCollection AddUserAuthentication(
        this IServiceCollection services)
    {
        //TODO - ADICIONAR A KEY EM OUTRO LUGAR
        var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(config =>
        {
           config.RequireHttpsMetadata = false;
           config.SaveToken = true;
           config.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false
           };
        });

        services.AddAuthorization(config =>
        {
            config.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new BearerRequirement())
                .Build();
        });        

        services.AddSingleton<IAuthorizationHandler,BearerAuthorizationHandler>();        

        return services;
    }
}
