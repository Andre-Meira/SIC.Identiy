using Identiy.Domain.Abstracts;

namespace Identiy.Domain.AgregattesModel.ValueObjects;

public record Address
{
    public Address(
        string rua,
        string numero,
        string cidade,
        string estado,
        string codigoPostal)
    {        
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CodigoPostal = codigoPostal;       
    }

    public string Rua { get; }
    public string Numero { get;}
    public string Cidade { get; }
    public string Estado { get; }
    public string CodigoPostal { get; }

    public void Validate(INotificationDomain notification)
    {        

        if (string.IsNullOrWhiteSpace(Rua))
            notification.AddNotification(new Notification("Rua", "Rua não poder ser nulo ou vazio"));

        if (string.IsNullOrWhiteSpace(Numero))
            notification.AddNotification(new Notification("Numero", "Numero não poder ser nulo ou vazio"));

        if (string.IsNullOrWhiteSpace(Cidade))
            notification.AddNotification(new Notification("Cidade", "Cidade não poder ser nulo ou vazio"));

        if (string.IsNullOrWhiteSpace(Estado))
            notification.AddNotification(new Notification("Estado", "Estado não poder ser nulo ou vazio"));

        if (string.IsNullOrWhiteSpace(CodigoPostal))
            notification.AddNotification(new Notification("CodigoPostal", "CodigoPostal não poder ser nulo ou vazio"));
    }

    public string FormatForDisplay()
    {
        return $"{Rua}, {Numero} - {Cidade}/{Estado}, {CodigoPostal}";
    }


}

