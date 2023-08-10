using Identiy.Domain.Abstracts;
using Identiy.Domain.AgregattesModel.ValueObjects;
using Identiy.Domain.Enums;

namespace Identiy.Domain.AgregattesModel.CompanyAgregattes.Entites;

public class Empresa : Entity
{
    private Endereco? _address;

    public Empresa(string nome, string cnpj) 
    {
        Name = nome;
        Cnpj = cnpj;
        DtCriacao = DateTime.Now;
        Status = Status.Enable;        
    }

    public string Name { get; private set; }
    public string Cnpj { get; private set; }    
    public DateTime DtCriacao { get; private set; }
    public Status Status { get; private set; }    

    public Endereco? Endereco => _address;

    public void CreatedAndress(Endereco address) => _address = address;
    public void Disable() => Status = Status.Disable;
}
