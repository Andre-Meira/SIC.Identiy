using Identiy.Domain.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Identiy.Domain.AgregattesModel.ValueObjects;

public record Endereco
{
    public Endereco(
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

        Validate();
    }

    public string Rua { get; }
    public string Numero { get;}
    public string Cidade { get; }
    public string Estado { get; }
    public string CodigoPostal { get; }


    public void Validate()
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Rua))
            errors.Add("Rua não poder vazio ou nulo.");

        if (string.IsNullOrWhiteSpace(Numero))
            errors.Add("Numero não poder vazio ou nulo.");

        if (string.IsNullOrWhiteSpace(Cidade))
            errors.Add("Cidade não poder vazio ou nulo");

        if (string.IsNullOrWhiteSpace(Estado))
            errors.Add("Estado não poder vazio ou nulo");

        if (string.IsNullOrWhiteSpace(CodigoPostal))
            errors.Add("CodigoPostal não poder vazio ou nulo");

        if (errors.Any() == false) throw new DomainExceptions(errors);
    }

    public string FormatForDisplay()
    {
        return $"{Rua}, {Numero} - {Cidade}/{Estado}, {CodigoPostal}";
    }


}

