using Identiy.Domain.Abstracts;

namespace Identiy.Domain.AgregattesModel.CompanyAgregattes.Events;


/// <summary>
/// Evento sera lançado quando uma nova empresa for cadastrada
/// </summary>
public struct CompanyCreated : IEvent
{    
    private readonly  decimal _id;

    public CompanyCreated(decimal id ,string companyName, string cnpj) 
        : this()
    {
        _id = id;
        CompanyName = companyName;
        Cnpj = cnpj;
    }

    /// <summary>
    /// ID da empresa criada
    /// </summary>
    public decimal Id => _id;
    public string CompanyName { get; }
    public string Cnpj { get;}
}