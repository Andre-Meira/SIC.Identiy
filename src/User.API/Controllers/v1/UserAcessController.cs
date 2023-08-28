using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations
using User.API.Models;
using User.Application.Commands;

namespace User.API.Controllers.v1
{
    [Route("api/user")]    
    [ApiController]
    public class UserAcessController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserAcessController(IMediator mediator) => _mediator = mediator;

        [HttpPost]        
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult> CreateUser([FromForm, Required] CreateUserCommand userCommand)
        {            
            await _mediator.Send(userCommand).ConfigureAwait(false);
            return Ok("Usuario criado com sucesso.");
        }
        
    }
}
