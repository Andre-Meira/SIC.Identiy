using Identiy.Domain.Abstracts;
using Identiy.Domain.AgregattesModel.CompanyAgregattes.Events;
using Identiy.Domain.AgregattesModel.ValueObjects;
using Identiy.Domain.Enums;

using System.Text.RegularExpressions;

namespace Identiy.Domain.AgregattesModel.CompanyAgregattes;

public class Company : Entity, IAggregate
{
    private Address? _andress;

    public Company(string nome, string cnpj)
    {
        Name = nome;
        Cnpj = cnpj;
        DtCreated = DateTime.Now;
        Status = Status.Enable;

        Validate();
        RaiseDomainEvent(new CompanyCreatedEventDomain(Id, Name, Cnpj));
    }

    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime DtCreated { get; }
    public Status Status { get; private set; }

    public Address? Endereco => _andress;


    public override void Validate()
    {
        if (Status == Status.Disable)
            throw new DomainExceptions("O registro está desativado, não é possivel alterá lo.");

        if (Endereco != null)
            Endereco.Validate(this);

        ValidaCnpj();
        ValidaNome();
        base.Validate();
    }

    public void ValidaCnpj()
    {
        if (string.IsNullOrEmpty(Cnpj))
        {
            AddNotification(new Notification("CNPJ", "Cnpj não pode ser vazio ou nulo"));
            return;
        }

        string pattern = @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$";
        if (Regex.IsMatch(Cnpj, pattern) == false)
            AddNotification(new Notification("CNPJ", "Cnpj não está no formato correto"));
    }

    public void ValidaNome()
    {
        if (string.IsNullOrEmpty(Name))
            AddNotification(new Notification("NOME", "Nome não pode ser nulo ou vazio"));
    }

    public void CreateAndress(Address address) => _andress = address;

    public void Disable()
    {
        Status = Status.Disable;
        RaiseDomainEvent(new CompanyDisabledEventDomain(Id, Name, Cnpj));
    }    
}

