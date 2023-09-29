using System.Net;

namespace User.API.Exceptions;

public class ExceptionRequest : Exception
{
    public string Mensagem { get; private set; }
    public HttpStatusCode StatusCode { get; private set; } 

    public object Data { get; private set; }

    public ExceptionRequest(string mensagem,
        object data,
        HttpStatusCode statusCode) : base(mensagem)
    {
        Mensagem = mensagem;
        StatusCode = statusCode;
        Data = data;
    }

}
