using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using User.Application.Commands;

namespace User.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAcessController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserAcessController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser([FromBody, Required] CreateUserCommand userCommand)
        {
            await _mediator.Send(userCommand).ConfigureAwait(false);
            return Ok("Sucesso.");
        }
        
    }
}
