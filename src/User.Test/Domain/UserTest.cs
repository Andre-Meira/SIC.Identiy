﻿using User.Domain.Abstracts;
using User.Domain.AgregattesModel;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Test.Domain;

public class UserTest
{

    [InlineData("")]
    [InlineData(null)]
    [InlineData("andremeira")]
    [InlineData("adadasd@.com")]
    [Theory(DisplayName = "Usuario Email Invalido")]
    [Trait("Usuario", "Email")]    
    public void Create_User_With_Invalid_Email_Thorows_DomainExecptions(string email)
    {
        Assert.Throws<DomainExceptions>(() => new UserAcess("Andre", email, "Teste11_123@MBA"));                
    }

    [InlineData("andre.meira@gmail.com")]
    [InlineData("andremeira@outlook.com")]
    [InlineData("ndremeira@d.com.br")]
    [Theory(DisplayName = "Usuario Email Invalido")]
    [Trait("Usuario", "Email")]
    public void Create_User_With_Valid_Email(string email)
    {
        new UserAcess("Andre", email, "Teste11_123@MBA");
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
