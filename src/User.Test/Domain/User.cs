using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Test.Domain;

public class User
{

    [InlineData("")]
    [InlineData(null)]
    [InlineData("andremeira")]
    [InlineData("adadasd@.com")]
    [Theory(DisplayName = "Usuario Email Invalido")]
    [Trait("Usuario", "Email")]    
    public void Create_User_With_Invalid_Email_Thorows_DomainExecptions(string email)
    {
        Assert.Throws<DomainExceptions>(() => new Attendant("André", email));
        Assert.Throws<DomainExceptions>(() => new Client("André", email));        
    }

    [InlineData("andre.meira@gmail.com")]
    [InlineData("andremeira@outlook.com")]
    [InlineData("ndremeira@d.com.br")]
    [Theory(DisplayName = "Usuario Email Invalido")]
    [Trait("Usuario", "Email")]
    public void Create_User_With_Valid_Email(string email)
    {
        new Attendant("André", email);
        new Client("André", email);
    }



    [InlineData("")]
    [InlineData(null)]
    [InlineData("andremeira")]
    [InlineData("adadasd@.com")]
    [Theory(DisplayName = "Usuario Password Invalida")]
    [Trait("Usuario", "Password")]
    public void Create_UserAcess_With_Invalid_Password_Thorows_DomainExecptions(string password)
    {
        Assert.Throws<DomainExceptions>(() => new UserAcess("Andre", "andre.meira@hotmail.com", password));        
    }




}
