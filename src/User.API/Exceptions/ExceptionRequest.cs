using System.Net;

namespace User.API.Exceptions;

public class ExceptionRequest : Exception
{
    public string Mensagem { get; private set; }
    public HttpStatusCode StatusCode { get; private set; } 

    public object Response { get; private set; }

    public ExceptionRequest(string mensagem,
        object response,
        HttpStatusCode statusCode) : base(mensagem)
    {
        Mensagem = mensagem;
        StatusCode = statusCode;
        Response = response;
    }

}
