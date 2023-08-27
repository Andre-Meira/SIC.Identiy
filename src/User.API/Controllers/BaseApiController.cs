using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using User.API.Models;

namespace User.API.Controllers
{
    
    public class BaseApiController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            ResultController resultController = new ResultController("Sucesso", 200, value);           
            return base.Ok(resultController);
        }
    }
}
