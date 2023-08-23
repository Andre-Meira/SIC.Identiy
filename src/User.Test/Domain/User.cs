using User.Domain.Abstracts;
using User.Domain.AgregattesModel.UserAgregattes;

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
        Assert.Throws<DomainExceptions>(() => new Attendant("André", email, "Tes@1098_ABC"));
        Assert.Throws<DomainExceptions>(() => new Client("André", email, "Tes@1098_ABC1"));        
    }
    

}
