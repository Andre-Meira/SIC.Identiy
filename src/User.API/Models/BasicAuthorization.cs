using Grpc.Core;
using System.Net;
using System.Text;
using User.API.Exceptions;
using User.Domain.Abstracts;

namespace User.API.Models;

public record BasicAuthorization
{
    public string? Username { get; }
    public string? Password { get; }


    public BasicAuthorization(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader)) 
            throw new ExceptionRequest("Acesso Invalido", 
                "Authorization não pode ser vazia.", 
                HttpStatusCode.Unauthorized);

        if (!IsBasicAuthorization(authorizationHeader))
        {
            throw new ExceptionRequest("Acesso Invalido", 
                $"Authorization não é do tipo Basic", 
                HttpStatusCode.Unauthorized);
        }

        var credentials = DecodeBasicCredentials(authorizationHeader);
        var parts = credentials.Split(':');

        if (parts.Length != 2)
        {
            throw new ExceptionRequest("Acesso Invalido",
                $"Credenciais inválidas no cabeçalho de autorização Basic", 
                HttpStatusCode.Unauthorized);
        }

        Username = parts[0];
        Password = parts[1];
    }

    private static bool IsBasicAuthorization(string authorizationHeader)
    {
        return authorizationHeader?.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase) == true;
    }

    private static string DecodeBasicCredentials(string authorizationHeader)
    {
        var base64Credentials = authorizationHeader.Substring(6);
        var credentialsBytes = Convert.FromBase64String(base64Credentials);
        return Encoding.UTF8.GetString(credentialsBytes);
    }
}
