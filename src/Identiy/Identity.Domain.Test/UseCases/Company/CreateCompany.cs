using Identiy.Domain.Abstracts;
using Identiy.Domain.AgregattesModel.CompanyAgregattes;
using Identiy.Domain.AgregattesModel.ValueObjects;

namespace Identity.Test.UseCases;

public class CreateCompany
{
    [Theory(DisplayName = "Cria empresa com endereco invalido")]
    [Trait("Empresa", "Endereco")]
    [InlineData("", "", "", "", "")]
    [InlineData(null, null, null, null, null)]
    [InlineData("", null, null, null, "21001239")]
    public void Cria_Empresa_Com_Endereco_Invalido_Retorna_Erro(
        string rua, string numero, string cidade,
        string estado, string codigoPosta)
    {
        Company company = new Company("Putucatu", "12.345.678/0001-00");
        Address address = new Address(rua, numero, cidade, estado, codigoPosta);
        company.CreateAndress(address);

        Assert.Throws<DomainExceptions>(() => company.Validate());
    }

    [Theory(DisplayName = "Cria empresa com endereco valido")]
    [Trait("Empresa", "Endereco")]
    [InlineData("Rua Santa", "05", "Cuiaba", "Mato Grosso", "2130590")]
    [InlineData("Rua Alberto", "123", "São Paulo", "São Paulo", "123120398")]
    public void Cria_Empresa_Com_Endereco_Valido_Retorna_Sucesso(
        string rua, string numero, string cidade,
        string estado, string codigoPosta)
    {
        Company empresa = new Company("Putucatu", "12.345.678/0001-00");
        Address endereco = new Address(rua, numero, cidade, estado, codigoPosta);
        empresa.CreateAndress(endereco);

        empresa.Validate();
    }


    [Theory(DisplayName = "Cria empresa com cnpj invalido")]
    [Trait("Empresa", "Cadastro Geral")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("1231231asdadasd")]
    public void Cria_Empresa_Com_Cnpj_Invalido_Retorna_Erro(string cnpj)
    {
        Assert.Throws<DomainExceptions>(() => new Company("Putucatu", cnpj));
    }


    [Theory(DisplayName = "Cria empresa com cnpj Valido")]
    [Trait("Empresa", "Cadastro Geral")]
    [InlineData("12.345.678/0001-00")]
    [InlineData("02.375.128/0103-01")]    
    public void Cria_Empresa_Com_Cnpj_Valido_Retorna_Sucesso(string cnpj)
    {
        new Company("Putucatu", cnpj);
    }
}
