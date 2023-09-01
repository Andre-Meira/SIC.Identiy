using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using User.API.Models;
using User.Application.Services;

namespace User.API.Controllers
{
    
    public class BaseApiController : ControllerBase
    {
        private JwtProvider JwtProvider 
            => JwtProvider.GetToken(HttpContext.User);

        protected Guid UserID => JwtProvider.Id;

        protected string? UserName => JwtProvider.UserName;
        protected string? Email => JwtProvider.Email;
        protected bool IsAutentication => JwtProvider.IsAuthenticate;       

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            ResultController resultController = new ResultController("Sucesso", 200, value);           
            return base.Ok(resultController);
        }
    }
}
